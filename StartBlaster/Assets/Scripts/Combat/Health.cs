using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool isAI = false;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioManager audioManager;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = AudioManager.instance; // fizemos singleton
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer) {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
            PlayHitParticles();

            if (applyCameraShake) {
                cameraShake.Play();
            }
        }
    }

    void TakeDamage(int value) {
        health -= value;
        if (health <= 0) {
            if (isAI) {
                scoreKeeper.ModifyScore(50);
            } else {
                levelManager.StartGameOver();
            }
            Destroy(gameObject);
        }
    }

    void PlayHitParticles() {
        if (hitParticles == null) { return; }

        ParticleSystem particles = Instantiate(
            hitParticles,
            transform.position,
            Quaternion.identity
        );
        Destroy(particles, particles.main.duration + particles.main.startLifetime.constantMax);
        audioManager.PlayDamageSFX();
    }

    public int GetHealth() {
        return health;
    }
}
