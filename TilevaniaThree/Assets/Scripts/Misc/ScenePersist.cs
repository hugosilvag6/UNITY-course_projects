using UnityEngine;

public class ScenePersist : MonoBehaviour {
    void Awake() {
        int scenePersists = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length;
        if (scenePersists > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist() {
        Destroy(gameObject);
    }
}
