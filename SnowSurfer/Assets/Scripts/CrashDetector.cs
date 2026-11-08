using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour {
    [SerializeField] ParticleSystem crashParticles;
    PlayerController playerController;

    private void Start() {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (other.gameObject.layer == layerIndex) {
            playerController.DisableControl();
            crashParticles.Play();
            Invoke("ReloadScene", 1f);
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
