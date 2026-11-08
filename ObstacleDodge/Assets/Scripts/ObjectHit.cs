using UnityEngine;

public class ObjectHit : MonoBehaviour {
    // Components
    private MeshRenderer meshRenderer;

    // Unity Lifecycle
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Events
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            meshRenderer.material.color = Color.red;
            gameObject.tag = "Hit";
        }
    }
}
