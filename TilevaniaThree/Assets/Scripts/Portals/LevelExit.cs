using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(LoadNextLevel(other));
    }

    IEnumerator LoadNextLevel(Collider2D other) {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == layerIndex) {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController?.EnteredPortal();
        }

        // O código abaixo é "segundos do jogo", então se a gente deixar o jogo mais rápido, esse tempo passa mais rápido.
        // yield return new WaitForSeconds(2);

        // Aqui é em segundos real life
        yield return new WaitForSecondsRealtime(1f);

        // Então chamamos o código
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
        FindAnyObjectByType<ScenePersist>().ResetScenePersist();
    }
}