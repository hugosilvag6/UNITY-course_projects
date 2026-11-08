using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour {
    // Configuration
    [SerializeField] private float moveSpeed = 10f;
    // Components
    private Rigidbody rb;
    // Input
    private InputAction moveAction;
    // State
    private Vector2 movementVector;

    // Unity Lifecycle
    private void Awake() {
        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Start() { }

    private void Update() {
        HandleInputs();
    }

    private void FixedUpdate() {
        Move();
    }

    // Private Methods
    private void HandleInputs() {
        movementVector = moveAction.ReadValue<Vector2>();
    }

    private void Move() {
        Vector3 movement = new Vector3(
            movementVector.x,
            0f,
            movementVector.y
        );

        Vector3 newPosition = rb.position + (moveSpeed * Time.fixedDeltaTime * movement);

        rb.MovePosition(newPosition);
    }
}
