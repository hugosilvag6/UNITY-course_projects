using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI levelText;

    // Unity Lifecycle
    private void Start() {
        progressBar.value = 0;

        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy() {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void Update() {
        UpdateProgressBar();
    }

    // Private Methods
    private void GameStateChangedCallback(GameManager.GameState gameState) {
        switch (gameState) {
            case GameManager.GameState.GameOver:
                ShowGameOver();
                break;
            case GameManager.GameState.LevelComplete:
                ShowLevelComplete();
                break;
            case GameManager.GameState.Menu:
            case GameManager.GameState.Game:
                break;
        }
    }

    public void PlayButtonPressed() {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar() {
        if (!GameManager.instance.IsGameState()) { return; }
        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishLineZ();
        progressBar.value = progress;
    }

    public void RetryButtonPressed() {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOver() {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    private void ShowLevelComplete() {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }
}
