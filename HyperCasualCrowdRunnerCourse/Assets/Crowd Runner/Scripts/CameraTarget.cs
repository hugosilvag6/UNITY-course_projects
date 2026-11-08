using UnityEngine;

public class CameraTarget : MonoBehaviour {
    [SerializeField] private Transform player;

    private void LateUpdate() {
        Vector3 position = transform.position;

        position.y = player.position.y;
        position.z = player.position.z;

        transform.position = position;
    }
}