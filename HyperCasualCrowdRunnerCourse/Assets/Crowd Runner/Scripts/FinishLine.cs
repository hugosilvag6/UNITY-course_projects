using UnityEngine;

public class FinishLine : MonoBehaviour {
    [Header("Elements")]
    [SerializeField] private GameObject finishLine;

    public float GetFinishLineZPosition() {
        return finishLine.transform.position.z;
    }
}
