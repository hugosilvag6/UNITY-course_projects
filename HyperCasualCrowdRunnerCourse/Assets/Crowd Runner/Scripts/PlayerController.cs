using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float roadWidth;
    private bool canMove;

    [Header("Control")]
    [SerializeField] private float slideSpeed = 5f;

    // Input
    private InputAction pointAction;
    private InputAction clickAction;

    // State
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    // Singleton
    public static PlayerController instance;

    // Unity Lifecycle
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;

            pointAction = InputSystem.actions.FindAction("Point");
            clickAction = InputSystem.actions.FindAction("Click");
        }
    }

    private void Start() {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void Update() {
        if (canMove) {
            MoveForward();
            ManageControl();
        }
    }

    private void OnDestroy() {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    // Methods
    private void MoveForward() {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void ManageControl() {
        Vector2 mousePosition = pointAction.ReadValue<Vector2>();

        // o "WasPressedThisFrame" pergunta:
        // -> esse botão começou a ser pressionado neste frame?
        // Então ele só dá true uma vez, no frame em que você aperta.
        if (clickAction.WasPressedThisFrame()) {
            clickedScreenPosition = mousePosition;
            clickedPlayerPosition = transform.position;

            // já o "IsPressed" pergunta:
            // esse botão está pressionado agora?
            // Então ele é true enquanto você segura:
        } else if (clickAction.IsPressed()) {
            float xScreenDifference = mousePosition.x - clickedScreenPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;
            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), roadWidth / 2 - crowdSystem.GetCrowdRadius());
            transform.position = position;
        }
    }

    private void GameStateChangedCallback(GameManager.GameState gameState) {
        switch (gameState) {
            case GameManager.GameState.Menu:
                break;
            case GameManager.GameState.Game:
                StartMoving();
                break;
            case GameManager.GameState.GameOver:
            case GameManager.GameState.LevelComplete:
                StopMoving();
                break;
        }
    }

    private void StartMoving() {
        canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving() {
        canMove = false;
        playerAnimator.Idle();
    }
}