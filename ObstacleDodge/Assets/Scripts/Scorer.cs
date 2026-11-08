using UnityEngine;

public class Scorer : MonoBehaviour {
    // State
    private int hits = 0;

    // Events
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Hit") {
            hits++;
        }
        print("Teste: " + hits);
    }
}
