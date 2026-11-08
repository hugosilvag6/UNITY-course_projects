using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    // Configuration
    [SerializeField] private int startingHealth = 3;
    // Components
    private Knockback knockback;
    private Flash flash;
    // References
    [SerializeField] private GameObject deathVFXPrefab;
    // State
    private int currentHealth;

    // Unity Lifecycle
    private void Awake() {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    // Private Methods
    private void HandleDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            PickupSpawner pickupSpawner = GetComponent<PickupSpawner>();
            if (pickupSpawner != null) {
                pickupSpawner.DropItems();
            }
            Destroy(gameObject);
        }
    }

    // Public Methods
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedback(PlayerController.Instance.transform, 15f);
        flash.FlashSprite();
        Invoke(nameof(HandleDeath), flash.GetFlashDuration());
    }
}
