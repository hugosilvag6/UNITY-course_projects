using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour {
    // Types
    private enum PickupType {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe
    }

    // Configuration
    [SerializeField] private PickupType pickupType;
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float accelerationRate = 0.2f;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 0.3f;
    // Components
    private Rigidbody2D rigidBody2D;
    // State
    private Vector3 moveDirection;

    // Unity Lifecycle
    private void Awake() {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(SpawnAnimationCurveRoutine());
    }

    private void Update() {
        Vector3 playerPosition = PlayerController.Instance.transform.position;

        bool isCloseToPlayer = Vector3.Distance(transform.position, playerPosition) < pickupDistance;
        if (isCloseToPlayer) {
            moveDirection = (playerPosition - transform.position).normalized;
            moveSpeed += accelerationRate;
        } else {
            moveDirection = Vector3.zero;
            moveSpeed = 0f;
        }
    }

    private void FixedUpdate() {
        rigidBody2D.linearVelocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    // Events
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    // Private Methods
    private IEnumerator SpawnAnimationCurveRoutine() {
        Vector3 startPosition = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-2f, 2f);
        Vector3 endPosition = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration) {
            timePassed += Time.deltaTime;

            float linearT = timePassed / popDuration;
            float heightT = animationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }
    }

    private void DetectPickupType() {
        switch (pickupType) {
            case PickupType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickupType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickupType.StaminaGlobe:
                Stamina.Instance.RefreshStamina();
                break;
        }
    }
}
