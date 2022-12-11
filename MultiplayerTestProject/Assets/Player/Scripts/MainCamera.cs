using UnityEngine;
using Cinemachine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    public void Init(Transform playerCameraLook, Transform playerAvatarShoulders)
    {
        _camera.Follow = playerAvatarShoulders;
        _camera.LookAt = playerCameraLook;
    }
}
