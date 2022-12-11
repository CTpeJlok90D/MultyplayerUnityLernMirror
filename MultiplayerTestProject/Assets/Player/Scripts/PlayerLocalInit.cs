using Mirror;
using UnityEngine;

public class PlayerLocalInit : NetworkBehaviour
{
    [SerializeField] private MainCamera _cameraPrefab;
    [SerializeField] private Transform _playerShoulders;
    [SerializeField] private Transform _playerCameraLookTarget;

    public void Start()
    {
        if (isLocalPlayer)
        {
            MainCamera camera = Instantiate(_cameraPrefab);
            camera.Init(_playerCameraLookTarget, _playerShoulders);
        }
    }
}
