using UnityEngine;

public class Chunk : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private Vector3 size;

    public float GetLength() {
        return size.z;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector3 position = new Vector3(
            transform.position.x,
            transform.position.y - 5,
            transform.position.z
        );
        Gizmos.DrawWireCube(position, size);
    }
}
