using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth> {
    // Configuration
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackThrustAmount = 10f;
    [SerializeField] private float damageTakingCooldown = 1f;
    // Components
    private Knockback knockback;
    private Flash flash;
    // State
    private int currentHealth;
    private bool damageTakingIsOnCooldown = false;
    // References
    private Slider healthSlider;

    // Unity Lifecycle
    protected override void Awake() {
        base.Awake();
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start() {
        currentHealth = maxHealth;

        UpdateHealthSlider();
    }

    // Events
    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy) {
            TakeDamage(1, other.transform);
        }
    }

    // Public Methods
    public void HealPlayer() {
        if (currentHealth >= maxHealth) { return; }

        currentHealth += 1;

        UpdateHealthSlider();
    }

    public void TakeDamage(int damageAmount, Transform damageSourceTransform) {
        if (damageTakingIsOnCooldown) { return; }

        damageTakingIsOnCooldown = true;
        currentHealth -= damageAmount;
        knockback.GetKnockedback(damageSourceTransform, knockbackThrustAmount);
        flash.FlashSprite();
        Invoke(nameof(EndDamageTakingCooldown), damageTakingCooldown);

        UpdateHealthSlider();
        CheckPlayerDeath();
    }

    // Private Methods
    private void EndDamageTakingCooldown() {
        damageTakingIsOnCooldown = false;
    }

    private void CheckPlayerDeath() {
        if (currentHealth <= 0) {
            print("Player has died!");
        }
    }

    private void UpdateHealthSlider() {
        if (healthSlider == null) {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
