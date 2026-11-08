using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveInventory : MonoBehaviour {
    // Input
    private InputAction selectSlot1Action;
    private InputAction selectSlot2Action;
    private InputAction selectSlot3Action;
    private InputAction selectSlot4Action;
    private InputAction selectSlot5Action;
    // State
    private int activeSlotIndex = 0;

    // Unity Lifecycle
    private void Awake() {
        // FindAction procura a Action no Input Actions Asset inteiro,
        // então encontraremos mesmo estando no Action Map "Inventory".
        selectSlot1Action = InputSystem.actions.FindAction("SelectSlot1");
        selectSlot2Action = InputSystem.actions.FindAction("SelectSlot2");
        selectSlot3Action = InputSystem.actions.FindAction("SelectSlot3");
        selectSlot4Action = InputSystem.actions.FindAction("SelectSlot4");
        selectSlot5Action = InputSystem.actions.FindAction("SelectSlot5");
    }

    private void Start() {
        SwitchActiveSlot(0);
    }

    private void Update() {
        HandleInputs();
    }

    private void OnEnable() {
        // Criar uma Action no Input Actions Asset não significa que ela esteja ativa.
        // Como todas essas Actions pertencem ao mapa "Inventory", habilitamos o mapa
        // inteiro quando este MonoBehaviour fica ativo.
        InputSystem.actions.FindActionMap("Inventory").Enable();
    }

    private void OnDisable() {
        // Quando este MonoBehaviour deixa de estar ativo, desabilitamos também
        // o mapa "Inventory". Se o objeto for ativado novamente, OnEnable
        // será chamado e o mapa voltará a processar os inputs.
        InputSystem.actions.FindActionMap("Inventory").Disable();
    }

    // Private Methods
    private void HandleInputs() {
        if (selectSlot1Action.WasPressedThisFrame()) {
            SwitchActiveSlot(0);
        } else if (selectSlot2Action.WasPressedThisFrame()) {
            SwitchActiveSlot(1);
        } else if (selectSlot3Action.WasPressedThisFrame()) {
            SwitchActiveSlot(2);
        } else if (selectSlot4Action.WasPressedThisFrame()) {
            SwitchActiveSlot(3);
        } else if (selectSlot5Action.WasPressedThisFrame()) {
            SwitchActiveSlot(4);
        }
    }

    private void SwitchActiveSlot(int newSlot) {
        activeSlotIndex = newSlot;

        InventorySlot newInventorySlot = transform.GetChild(newSlot).GetComponent<InventorySlot>();

        HandleSlotHighlights();
        HandleActiveWeapon(newInventorySlot);
    }

    private void HandleSlotHighlights() {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            InventorySlot slot = child.GetComponent<InventorySlot>();

            slot.ToggleActive(i == activeSlotIndex);
        }
    }

    private void HandleActiveWeapon(InventorySlot newInventorySlot) {
        WeaponInfoSO weaponInfo = newInventorySlot.GetWeaponInfoSO();

        ActiveWeapon.Instance.SetActiveWeapon(weaponInfo);
    }
}