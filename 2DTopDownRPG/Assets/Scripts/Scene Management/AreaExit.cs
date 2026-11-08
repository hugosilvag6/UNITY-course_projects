using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine() {
        UIFade.Instance.FadeIn();

        yield return new WaitForSeconds(1f);

        SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        SceneManager.LoadScene(sceneToLoad);
    }
}
