using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour {
    // Configuration
    private float transparencyAmount = 0.7f;
    private float fadeTime = 0.4f;
    // Components
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;
    // States
    private Coroutine fadeCoroutine;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            StartFade(transparencyAmount);
            // Daria pra setar o alpha aqui logo, e no onExit setar o normal
            // Porém a abordagem usada com routine faz o fade aos poucos, tipo com "transição"
            // spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, transparencyAmount);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            StartFade(1f);
        }
    }

    private void StartFade(float targetValue) {
        if (fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }

        if (spriteRenderer) {
            fadeCoroutine = StartCoroutine(FadeRoutine(spriteRenderer, targetValue));
        } else if (tilemap) {
            fadeCoroutine = StartCoroutine(FadeRoutine(tilemap, targetValue));
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float targetValue) {
        float startValue = spriteRenderer.color.a;
        float elapsedTime = 0;
        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetValue, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float targetValue) {
        float startValue = tilemap.color.a;
        float elapsedTime = 0;
        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetValue, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}