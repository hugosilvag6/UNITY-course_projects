using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour {
    // Input
    private InputAction pointAction;

    // Unity Lifecycle
    private void Awake() {
        pointAction = InputSystem.actions.FindAction("Point");
    }

    private void Start() {
        // Cursor.visible = false;
    }

    private void Update() {
        Vector2 mousePosition = pointAction.ReadValue<Vector2>();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f;
        transform.position = worldPosition;
    }
}
