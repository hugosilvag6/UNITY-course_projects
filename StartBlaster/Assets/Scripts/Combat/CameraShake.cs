using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    float shakeDuration = 0.3f;
    float shakeMagnitude = 0.2f;

    Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
    }

    public void Play() {
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera() {
        float timeElapsed = 0;
        while (timeElapsed < shakeDuration) {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
