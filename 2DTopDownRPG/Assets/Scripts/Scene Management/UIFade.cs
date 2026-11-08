using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade> {
    [SerializeField] private Image fadeScreen;
    private float fadeSpeed = 1f;

    private Coroutine fadeRoutine;

    public void FadeIn() {
        if (fadeRoutine != null) {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = StartCoroutine(FadeRoutine(1));
    }

    public void FadeOut() {
        if (fadeRoutine != null) {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = StartCoroutine(FadeRoutine(0));
    }

    private IEnumerator FadeRoutine(float targetAlpha) {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha)) {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}
