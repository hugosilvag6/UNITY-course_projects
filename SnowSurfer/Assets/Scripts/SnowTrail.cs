using UnityEngine;

public class SnowTrail : MonoBehaviour {
    [SerializeField] ParticleSystem snowParticles;

    private void OnCollisionEnter2D(Collision2D other) {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (other.gameObject.layer == layerIndex) {
            snowParticles.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (other.gameObject.layer == layerIndex) {
            snowParticles.Stop();
        }
    }
}
