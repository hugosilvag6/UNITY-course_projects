using UnityEngine;

public class Destructible : MonoBehaviour {
    // Configuration
    [SerializeField] private GameObject destroyVFXPrefab;

    // Public Methods
    public void DestroySelf() {
        Instantiate(destroyVFXPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

        PickupSpawner pickupSpawner = GetComponent<PickupSpawner>();
        if (pickupSpawner != null) {
            pickupSpawner.DropItems();
        }
    }
}
