using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour {
    // Configuration
    [SerializeField] private float flashDuration = 0.2f;
    // Components
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    // References
    [SerializeField] private Material whiteFlashMaterial;

    // Unity Lifecycle
    private void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }

    // Private Methods
    private IEnumerator FlashRoutine() {
        spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = defaultMaterial;
    }

    // Public Methods
    public void FlashSprite() {
        StartCoroutine(FlashRoutine());
    }

    public float GetFlashDuration() {
        return flashDuration;
    }
}
