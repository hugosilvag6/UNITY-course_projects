using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour {
    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;

    [Header("Events")]
    public static Action onDoorsHit;

    private void Update() {
        if (!GameManager.instance.IsGameState()) { return; }
        DetectDoors();
    }

    // Aparentemente, essa abordagem é mais ideal para quando queremos fazer uma pergunta pontual ao sistema de física
    // Quando temos uma área permanente e queremos receber eventos dela, supostamente é melhor o OnTrigger
    private void DetectDoors() {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);

        for (int i = 0; i < detectedColliders.Length; i++) {
            if (detectedColliders[i].TryGetComponent(out Doors doors)) {
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.Disable();

                onDoorsHit?.Invoke();

                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            } else if (detectedColliders[i].tag == "Finish") {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                // SceneManager.LoadScene(0);
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
            }
        }
    }
}
