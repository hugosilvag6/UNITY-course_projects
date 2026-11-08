using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour {
    // // Input
    // private InputAction pointAction;

    // // Unity Lifecycle
    // private void Awake() {
    //     pointAction = InputSystem.actions.FindAction("Point");
    // }

    // private void Update() {
    //     FaceMouse();
    // }

    // // Private Methods
    // private void FaceMouse() {
    //     Vector2 mousePosition = pointAction.ReadValue<Vector2>();
    //     mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

    //     Vector2 direction = transform.position - (Vector3)mousePosition;

    //     transform.right = -direction;
    // }
}
