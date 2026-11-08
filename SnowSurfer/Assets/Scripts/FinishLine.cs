using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {
    [SerializeField] ParticleSystem finishParticles;

    private void OnTriggerEnter2D(Collider2D other) {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == layerIndex) {
            finishParticles.Play();
            Invoke("ReloadScene", 1f);
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
