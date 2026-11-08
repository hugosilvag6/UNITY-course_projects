using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    float torqueAmount = 2f;

    float baseSpeed = 15f;
    float boostSpeed = 30f;
    SurfaceEffector2D surfaceEffector2D;
    InputAction moveAction;
    Rigidbody2D rigidBody;
    Vector2 moveVector;
    bool canControlPlayer = true;
    float previousRotation;
    float totalRotation;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] ParticleSystem powerupParticles;
    int activePowerups = 0;

    void Start() {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
    }

    void Update() {
        if (!canControlPlayer) { return; }

        moveVector = moveAction.ReadValue<Vector2>();
        RotatePlayer();
        BoostPlayer();
        CalculateFlips();
    }

    void RotatePlayer() {
        // Estamos adicionando torque, e "não há resistência" contra ele, então isso vai ficar aumentando sem parar.
        // Pra resolver isso, podemos na IDE, no RigidBody adicionar Angular Damping. É como uma resistência a rotação.
        if (moveVector.x < 0) {
            rigidBody.AddTorque(torqueAmount);
        } else if (moveVector.x > 0) {
            rigidBody.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer() {
        if (moveVector.y > 0) {
            surfaceEffector2D.speed = boostSpeed;
        } else {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void CalculateFlips() {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);

        if (totalRotation > 340 || totalRotation < -340) {
            totalRotation = 0;
            scoreManager.AddScore(100);
        }

        previousRotation = currentRotation;
    }

    public void DisableControl() {
        canControlPlayer = false;
    }

    public void ActivatePowerup(PowerupSO powerup) {
        switch (powerup.GetPowerupType()) {
            case "speed":
                baseSpeed += powerup.GetValueChange();
                boostSpeed += powerup.GetValueChange();
                break;
            case "torque":
                torqueAmount += powerup.GetValueChange();
                break;
        }
        powerupParticles.Play();
        activePowerups += 1;
    }

    public void DeactivatePowerup(PowerupSO powerup) {
        switch (powerup.GetPowerupType()) {
            case "speed":
                baseSpeed -= powerup.GetValueChange();
                boostSpeed -= powerup.GetValueChange();
                break;
            case "torque":
                torqueAmount -= powerup.GetValueChange();
                break;
        }

        activePowerups -= 1;
        if (activePowerups <= 0) {
            powerupParticles.Stop();
        }
    }
}
