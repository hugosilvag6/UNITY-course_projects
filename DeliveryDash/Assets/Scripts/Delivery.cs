using UnityEngine;

public class Delivery: MonoBehaviour {
    [SerializeField] float destroyDelay = 0.1f;
    bool hasPackage = false;

    void Start() {}
    void Update() {}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Package") && !hasPackage) {
            hasPackage = true;
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject, destroyDelay);
        }

        if (other.CompareTag("Customer") && hasPackage) {
            GetComponent<ParticleSystem>().Stop();
            hasPackage = false;
            Destroy(other.gameObject);
        }
    }
}
