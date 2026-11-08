using System.Collections;
using UnityEngine;

public class SpriteFadeAndDestroy : MonoBehaviour {
    // Configuration
    [SerializeField] private float fadeTime = 0.3f;
    // Components
    private SpriteRenderer spriteRenderer;

    // Unity Lifecycle
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Private Methods
    public void FadeAndDestroy() {
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine() {
        float alphaReductionPerSecond = spriteRenderer.color.a / fadeTime;

        while (spriteRenderer.color.a > 0f) {
            // Acontecerá uma vez por frame (por causa do yield return null)
            // Logo, o que queremos reduzir em cada iteração não é alphaReductionPerSecond
            // é alphaReductionPerSecond dividido por quantos frames tiveram naquele segundo
            float newAlpha = spriteRenderer.color.a - alphaReductionPerSecond * Time.deltaTime;

            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                Mathf.Max(newAlpha, 0f) // só pra garantir que o Alpha não fique negativo
            );

            yield return null;
        }

        Destroy(gameObject);
    }
}

