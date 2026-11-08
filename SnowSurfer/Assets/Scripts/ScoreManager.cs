using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;

    public void AddScore(int value) {
        score += value;
        scoreText.text = "Score: " + score;
    }
}
