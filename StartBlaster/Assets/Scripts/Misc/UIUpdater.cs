using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour {
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] Health playerHealth;

    ScoreKeeper scoreKeeper;

    private void Awake() {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    private void Start() {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    private void Update() {
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        healthSlider.value = playerHealth.GetHealth();
    }
}
