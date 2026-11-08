using UnityEngine;

public class Projectile : MonoBehaviour {
    // Configuration
    private int damage;
    private float maxRange;
    private float moveSpeed;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    // State
    private Vector3 startPosition;

    // Unity Lifecycle
    private void Start() {
        startPosition = transform.position;
    }

    private void Update() {
        MoveProjectile();
        DestroyAfterReachingMaxRange();
    }

    // Inits
    public void Initialize(
        int damage,
        float maxRange,
        float moveSpeed
    ) {
        this.damage = damage;
        this.maxRange = maxRange;
        this.moveSpeed = moveSpeed;
    }

    // Events
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        Destructible destructible = other.gameObject.GetComponent<Destructible>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

        if (enemy && !isEnemyProjectile) {
            enemy.TakeDamage(damage);
        }

        if (player && isEnemyProjectile) {
            player.TakeDamage(damage, this.transform);
        }

        if (destructible) {
            destructible.DestroySelf();
        }

        if (
            indestructible ||
            destructible ||
            enemy && !isEnemyProjectile ||
            player && isEnemyProjectile
        ) {
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    // Private Methods
    private void MoveProjectile() {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
    }

    private void DestroyAfterReachingMaxRange() {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);

        if (distanceTravelled >= maxRange) {
            Destroy(gameObject);
        }
    }
}
