using UnityEngine;

public class InventorySlot : MonoBehaviour {
    // Components
    [SerializeField] private GameObject activeHighlight;
    [SerializeField] private WeaponInfoSO weaponInfoSO;

    // Public Methods
    public void ToggleActive(bool isActive) {
        activeHighlight.SetActive(isActive);
    }

    public WeaponInfoSO GetWeaponInfoSO() {
        return weaponInfoSO;
    }
}
