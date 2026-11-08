using System.Collections;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour {
    // Configuration
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject grapeProjectileShadowPrefab;
    [SerializeField] private GameObject grapeLandSplatterPrefab;

    private void Start() {
        StartCoroutine(ProjectileCurveRoutine());
        StartCoroutine(ProjectileShadowRoutine());
    }

    private IEnumerator ProjectileCurveRoutine() {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = PlayerController.Instance.transform.position;

        float timePassed = 0f;

        while (timePassed < duration) {
            timePassed += Time.deltaTime;

            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }

        Destroy(gameObject);
        SpawnGrapeLandSplatter();
    }

    private IEnumerator ProjectileShadowRoutine() {
        GameObject grapeShadow = Instantiate(
            grapeProjectileShadowPrefab,
            transform.position + new Vector3(0, -0.3f, 0),
            transform.rotation
        );
        Vector3 startPosition = grapeShadow.transform.position;
        Vector3 endPosition = PlayerController.Instance.transform.position;

        float timePassed = 0f;
        while (timePassed < duration) {
            timePassed += Time.deltaTime;

            grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, timePassed / duration);

            yield return null;
        }

        Destroy(grapeShadow);
    }

    private void SpawnGrapeLandSplatter() {
        Instantiate(
            grapeLandSplatterPrefab,
            this.transform.position,
            this.transform.rotation
        );
    }
}
