using UnityEngine;

public class SoundsManager : MonoBehaviour {
    [Header("Sounds")]
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;

    private void Start() {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
        GameManager.onGameStateChanged += GameStateChangedCallback;
        Enemy.onRunnerDied += PlayRunnerDieSound;
    }

    private void OnDestroy() {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        Enemy.onRunnerDied -= PlayRunnerDieSound;
    }

    private void PlayDoorHitSound() {
        doorHitSound.Play();
    }

    private void PlayRunnerDieSound() {
        runnerDieSound.Play();
    }

    private void GameStateChangedCallback(GameManager.GameState gameState) {
        switch (gameState) {
            case GameManager.GameState.Menu:
            case GameManager.GameState.Game:
                break;
            case GameManager.GameState.GameOver:
                gameOverSound.Play();
                break;
            case GameManager.GameState.LevelComplete:
                levelCompleteSound.Play();
                break;
        }
    }
}
