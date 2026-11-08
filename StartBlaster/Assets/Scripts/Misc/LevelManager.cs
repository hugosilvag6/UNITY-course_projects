using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("GameScene");
        ScoreKeeper shared = ScoreKeeper.instance;
        shared.ResetScore();
    }

    public void StartGameOver() {
        StartCoroutine(StartSceneWithDelay("GameOver", 1f));
    }

    public void StartMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator StartSceneWithDelay(string scene, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}
