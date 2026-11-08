using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    // References
    // OBS:
    // O Canvas está dentro do GameSession, então ele "herda" a característica singleton
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    // States
    int playerLives = 3;
    int playerScore = 0;

    void Awake() {
        int gameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (gameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        UpdateLivesText();
    }

    void ReloadScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void UpdateLivesText() {
        livesText.text = playerLives.ToString();
    }

    public void ProcessPlayerDeath() {
        if (playerLives > 1) {
            playerLives -= 1;
            Invoke("ReloadScene", 1f);
            Invoke("UpdateLivesText", 1f);
        } else {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
            FindAnyObjectByType<ScenePersist>().ResetScenePersist();
        }
    }

    public void ProccessCoinCollected() {
        playerScore += 100;
        scoreText.text = playerScore.ToString();
    }
}