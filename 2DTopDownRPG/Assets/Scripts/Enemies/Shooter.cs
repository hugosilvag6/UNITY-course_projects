using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy {
    // Configuration
    // Bullets
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletMaxRange;
    [SerializeField] private float bulletMoveSpeed;
    // Waves
    [SerializeField] private int bulletsPerWave;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField, Min(0.01f)] private float startingDistance;
    // Attack
    [SerializeField] private bool stagger;
    [Tooltip("Stagger has to be enabled for oscillate to work properly.")]
    [SerializeField] private bool oscillate;
    [SerializeField] private int attackWaves;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField, Min(0.01f)] private float attackCooldown = 3f;

    // References
    [SerializeField] private GameObject bulletPrefab;

    // Unity Lifecycle
    private void OnValidate() {
        if (oscillate) { stagger = true; }
        if (!oscillate) { stagger = false; }
        if (bulletsPerWave < 1) { bulletsPerWave = 1; }
        if (attackWaves < 1) { attackWaves = 1; }
        if (timeBetweenWaves < 0.1f) { timeBetweenWaves = 0.1f; }
        if (attackCooldown < 0.1f) { attackCooldown = 0.1f; }
        if (angleSpread == 0) { bulletsPerWave = 1; }
        if (bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }
    }

    // IEnemy Implementation
    public IEnumerator Attack() {
        TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle);

        float timeBetweenProjectiles = 0f;
        if (stagger) {
            timeBetweenProjectiles = timeBetweenWaves / bulletsPerWave;
        }

        for (int i = 0; i < attackWaves; i++) {
            if (!oscillate) {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1) {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            } else if (oscillate) {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            for (int j = 0; j < bulletsPerWave; j++) {
                Vector2 position = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(
                    bulletPrefab,
                    position,
                    transform.rotation
                );
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile)) {
                    projectile.Initialize(bulletDamage, bulletMaxRange, bulletMoveSpeed);
                }

                currentAngle += angleStep;

                if (stagger) {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }

            currentAngle = startAngle;

            if (!stagger && i < attackWaves - 1) {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
    }

    // Private Methods
    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle) {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (angleSpread != 0) {
            angleStep = angleSpread / (bulletsPerWave - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle) {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        return new Vector2(x, y);
    }
}

