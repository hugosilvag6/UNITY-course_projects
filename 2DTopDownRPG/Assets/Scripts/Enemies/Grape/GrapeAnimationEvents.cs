using UnityEngine;

public class GrapeAnimationEvents : MonoBehaviour {
    // References
    private Grape grape;

    // Unity Lifecycle
    private void Awake() {
        grape = GetComponentInParent<Grape>();
    }

    // Animation Events
    public void SpawnProjectileAnimationEvent() {
        grape.SpawnProjectileAnimationEvent();
    }
}
