using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina> {
    // Public Values
    public int CurrentStamina { get; private set; }
    // Configuration
    private int startingStamina = 3;
    private int maxStamina;
    [SerializeField] private int timeToRefreshStamina = 3;
    // References
    private Transform staminaContainer;
    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    const string STAMINA_CONTAINER = "Stamina Container";

    // Unity Lifecycle
    protected override void Awake() {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

    private void Start() {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER).transform;
    }

    // Public Methods
    public void UseStamina() {
        CurrentStamina -= 1;
        UpdateStaminaImages();
    }

    public void RefreshStamina() {
        if (CurrentStamina >= maxStamina) { return; }

        CurrentStamina += 1;
        UpdateStaminaImages();
    }

    // Private Methods
    private void UpdateStaminaImages() {
        for (int i = 0; i < maxStamina; i++) {
            Image image = staminaContainer.GetChild(i).GetComponent<Image>();
            if (image != null) {
                if (i <= CurrentStamina - 1) {
                    image.sprite = fullStaminaImage;
                } else {
                    image.sprite = emptyStaminaImage;
                }
            }
        }

        if (CurrentStamina < maxStamina) {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }

    private IEnumerator RefreshStaminaRoutine() {
        while (true) {
            yield return new WaitForSeconds(timeToRefreshStamina);
            RefreshStamina();
        }
    }
}
