using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // Configuration
    float moveSpeed = 10f;
    float topPadding = 4f; // Usado pra evitar que fique muito próximo dos inimigos

    // Components
    SpriteRenderer sprite;
    Shooter shooter;

    // Inputs
    InputAction moveAction;
    InputAction fireAction;

    // States
    Vector2 movementVector;
    Vector2 minCameraBounds;
    Vector2 maxCameraBounds;

    // Cached Values

    // Unity Lifecycle
    void Awake() {
        sprite = GetComponentInChildren<SpriteRenderer>();
        shooter = GetComponent<Shooter>();

        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Attack");
    }

    void Start() {
        InitBounds();
    }

    void Update() {
        MovePlayer();
        FireShooter();
    }

    // Actions
    void MovePlayer() {
        movementVector = moveAction.ReadValue<Vector2>();

        float width = sprite.bounds.size.x;
        float height = sprite.bounds.size.y;

        Vector3 newPosition = transform.position + new Vector3(
            moveSpeed * Time.deltaTime * movementVector.x,
            moveSpeed * Time.deltaTime * movementVector.y,
            0f
        );
        // O clamp limita o valor entre min e max. Ele recebe: (valor, min, max)
        newPosition.x = Math.Clamp(newPosition.x, minCameraBounds.x + width / 2, maxCameraBounds.x - width / 2);
        newPosition.y = Math.Clamp(newPosition.y, minCameraBounds.y + height / 2, maxCameraBounds.y - height / 2 - topPadding);
        transform.position = newPosition;
    }

    void FireShooter() {
        shooter.isFiring = fireAction.IsPressed();
    }

    // Helpers
    void InitBounds() {
        Camera mainCamera = Camera.main; // Câmera principal da cena no momento
        minCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}