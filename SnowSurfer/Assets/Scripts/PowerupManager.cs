using UnityEngine;

public class PowerupManager : MonoBehaviour {
    [SerializeField] PowerupSO powerup;

    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    float timeLeft;

    private void Start() {
        playerController = FindAnyObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = powerup.GetTime();
    }

    private void Update() {
        CountdownTimer();
    }

    private void CountdownTimer() {
        if (!spriteRenderer.enabled) {
            if (timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0) {
                    playerController.DeactivatePowerup(powerup);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == layerIndex && spriteRenderer.enabled) {
            playerController.ActivatePowerup(powerup);
            spriteRenderer.enabled = false;
        }
    }
}
