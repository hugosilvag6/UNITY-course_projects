using UnityEngine;

public class Parallax : MonoBehaviour {
    // Configuration
    [SerializeField] private float parallaxOffset = -0.15f;
    // Components
    private Camera camera;
    // States and Values
    private Vector2 startPosition;
    private Vector2 travel => (Vector2)camera.transform.position - startPosition;

    private void Awake() {
        camera = Camera.main;
        startPosition = transform.position;
    }

    private void FixedUpdate() {
        transform.position = startPosition + travel * parallaxOffset;
    }
}
