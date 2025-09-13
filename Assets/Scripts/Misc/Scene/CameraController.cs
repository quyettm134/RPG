using Unity.Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineCamera cinemachineCamera;

    public void SetCameraFollow()
    {
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
        cinemachineCamera.Follow = Player.Instance.transform;
    }
}
