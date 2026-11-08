using UnityEngine;

public class Runner : MonoBehaviour {
    [Header("Settings")]
    private bool isTarget;

    public void SetTarget() {
        isTarget = true;
    }

    public bool IsTarget() {
        return isTarget;
    }
}
