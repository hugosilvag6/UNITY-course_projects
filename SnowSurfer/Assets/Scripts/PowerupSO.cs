using UnityEngine;

// Definem o "atalho" para criar o ScriptableObject desse arquivo. Esse atalho fica no botão direito.
[CreateAssetMenu(fileName = "Powerup", menuName = "PowerupSO")]
public class PowerupSO : ScriptableObject {
    [SerializeField] string powerupType;
    [SerializeField] float valueChange;
    [SerializeField] float time;

    public string GetPowerupType() {
        return powerupType;
    }

    public float GetValueChange() {
        return valueChange;
    }

    public float GetTime() {
        return time;
    }
}
