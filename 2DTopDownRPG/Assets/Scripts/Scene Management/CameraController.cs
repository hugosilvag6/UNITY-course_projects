using Unity.Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController> {
    // Components
    private CinemachineCamera cinemachineCamera;

    // Unity Lifecycle
    private void Start() {
        SetPlayerCameraFollow();
    }

    // Public Methods
    public void SetPlayerCameraFollow() {
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
        cinemachineCamera.Follow = PlayerController.Instance.transform;
    }
}
