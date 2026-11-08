using UnityEngine;

public class Knockback : MonoBehaviour {
    public bool gettingKnockedBack { get; private set; }
    // Configuration
    private float knockbackDuration = 0.2f;
    // Components
    private Rigidbody2D rb;

    // Unity Lifecycle
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Public Methods
    public void GetKnockedback(Transform damageSource, float knockbackThrust) {
        gettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        Invoke(nameof(EndKnockback), knockbackDuration);
    }

    private void EndKnockback() {
        rb.linearVelocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
