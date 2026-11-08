using UnityEngine;

public class HazardsManager : MonoBehaviour {
    // References
    PlayerController playerController;

    void Awake() {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == layerIndex) {
            playerController.TakeDamage();
        }
    }
}
