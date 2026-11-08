using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController> {
    // Configuration
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeedMultiplier = 4f;
    // Components
    private Rigidbody2D rb;
    private Knockback knockback;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    // Input
    private InputAction moveAction;
    private InputAction pointAction;
    private InputAction dashAction;
    // State
    private Vector2 movementVector;
    private bool isDashing = false;
    // Cached Values

    // Unity Lifecycle
    protected override void Awake() {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();

        moveAction = InputSystem.actions.FindAction("Move");
        pointAction = InputSystem.actions.FindAction("Point");
        dashAction = InputSystem.actions.FindAction("Dash");
    }

    private void Update() {
        HandleInputs();
        HandleSprite();
        Dash();
    }

    private void FixedUpdate() {
        Move();
    }

    // Actions
    private void HandleInputs() {
        movementVector = moveAction.ReadValue<Vector2>();
    }

    private void Move() {
        if (knockback.gettingKnockedBack) { return; }

        var newPosition = rb.position + (moveSpeed * Time.deltaTime * movementVector);
        rb.MovePosition(newPosition);
    }

    private void HandleSprite() {
        // Animation
        myAnimator.SetFloat("moveX", movementVector.x);
        myAnimator.SetFloat("moveY", movementVector.y);

        // Facing direction
        var myPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePosition = pointAction.ReadValue<Vector2>();
        mySpriteRenderer.flipX = mousePosition.x < myPosition.x;
    }

    private void Dash() {
        if (!dashAction.WasPressedThisFrame()) { return; }

        if (isDashing) return;
        if (Stamina.Instance.CurrentStamina <= 0) return;

        moveSpeed *= dashSpeedMultiplier;
        myTrailRenderer.emitting = true;
        isDashing = true;
        Stamina.Instance.UseStamina();
        StartCoroutine(EndDash());
    }

    private IEnumerator EndDash() {
        yield return new WaitForSeconds(0.2f);
        moveSpeed /= dashSpeedMultiplier;
        myTrailRenderer.emitting = false;

        yield return new WaitForSeconds(1f);
        isDashing = false;
    }
}