using UnityEngine;

public class ScoreKeeper : MonoBehaviour {
    int score = 0;

    // Singleton handling
    public static ScoreKeeper instance;

    void ManageSingleton() {
        // int instanceCount = FindObjectsByType<ScoreKeeper>(FindObjectsSortMode.None).Length;
        // if (instanceCount > 1) {
        if (instance != null) {
            // setactive tá aqui pra evitar que seja possível usar esse objeto antes de destruir.
            // O Destroy não destrói sempre no momento que chama. pode levar algum tempo,
            // pois ele na verdade "agenda" a destruição pro fim do frame
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Awake() {
        ManageSingleton();
    }

    public int GetScore() {
        return score;
    }

    public void ModifyScore(int value) {
        int newValue = score + value;
        score = Mathf.Max(0, newValue);
    }

    public void ResetScore() {
        score = 0;
    }
}
