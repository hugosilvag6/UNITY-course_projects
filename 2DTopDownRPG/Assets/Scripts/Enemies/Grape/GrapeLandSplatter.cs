using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour {
    // Components
    private SpriteFadeAndDestroy spriteFadeAndDestroy;

    // Unity Lifecycle
    private void Awake() {
        spriteFadeAndDestroy = GetComponent<SpriteFadeAndDestroy>();
    }

    private void Start() {
        spriteFadeAndDestroy.FadeAndDestroy();
    }

    // Events
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null) {
            playerHealth.TakeDamage(1, this.transform);
        }
    }
}
