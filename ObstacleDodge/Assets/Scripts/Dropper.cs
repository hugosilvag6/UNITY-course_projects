using UnityEngine;

public class Dropper : MonoBehaviour {
    // Configuration
    [SerializeField] private float timeToWait = 2f;
    // Components
    private Rigidbody rb;
    private MeshRenderer meshRenderer;

    // Unity Lifecycle
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() {
        rb.useGravity = false;
        meshRenderer.enabled = false;
    }

    private void Update() {
        if (Time.time > timeToWait) {
            rb.useGravity = true;
            meshRenderer.enabled = true;
        }
    }
}
