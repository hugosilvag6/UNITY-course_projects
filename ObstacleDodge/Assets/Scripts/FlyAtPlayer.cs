using UnityEngine;

public class FlyAtPlayer : MonoBehaviour {
    // Configuration
    [SerializeField] private float speed = 10f;
    // References
    [SerializeField] private Transform player;
    // State
    private Vector3 targetPosition;

    private void Start() {
        targetPosition = player.position;
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }
}
