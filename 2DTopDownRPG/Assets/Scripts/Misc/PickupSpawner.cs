using UnityEngine;

public class PickupSpawner : MonoBehaviour {
    // References
    [SerializeField] private GameObject goldCoin, healthGlobe, staminaGlobe;

    // Public Methods
    public void DropItems() {
        int randomNumber = Random.Range(1, 4); // Randomly choose between 1 and 3

        switch (randomNumber) {
            case 1:
                int randomGoldAmount = Random.Range(1, 4);
                for (int i = 0; i < randomGoldAmount; i++) {
                    InstantiatePickup(goldCoin);
                }
                break;
            case 2:
                InstantiatePickup(healthGlobe);
                break;
            case 3:
                InstantiatePickup(staminaGlobe);
                break;
        }
    }

    // Private Methods
    private void InstantiatePickup(GameObject prefab) {
        Instantiate(
            prefab,
            transform.position,
            transform.rotation
        );
    }
}
