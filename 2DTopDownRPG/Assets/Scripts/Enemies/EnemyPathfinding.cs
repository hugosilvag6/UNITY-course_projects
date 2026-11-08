using UnityEngine;

public class EnemyPathfinding : MonoBehaviour {
    // Configuration
    [SerializeField] private float moveSpeed = 4f;
    // Components
    private Rigidbody2D rb;
    private Knockback knockback;
    // State
    private Vector2 targetPosition;

    // Unity Lifecycle
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    private void FixedUpdate() {
        Move();
    }

    // Actions
    private void Move() {
        if (knockback.gettingKnockedBack) { return; }
        if (targetPosition == null || targetPosition == Vector2.zero) { return; }

        var newPosition = rb.position + (moveSpeed * Time.deltaTime * targetPosition);
        rb.MovePosition(newPosition);

        FlipSpriteAndShadow();
    }

    private void FlipSpriteAndShadow() {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
            spriteRenderer.flipX = targetPosition.x < 0;
        }
    }

    // Public methods
    public void SetTargetPosition(Vector2 targetPosition) {
        this.targetPosition = targetPosition;
    }

    public void StopMoving() {
        this.targetPosition = Vector2.zero;
    }
}
