using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{
    [SerializeField] private CharacterController _charancterController;
    [SerializeField] private float _moveSpeed = 2f;
    [SyncVar]
    [SerializeField] private string _nickname = "Player";
    [SerializeField] private Transform _shouders;
    [SerializeField] private Vector2 _maxCameraRotaion;
    [SerializeField] private Vector2 _minCameraRotation;

    [SyncVar] private Vector3 _currentMoveDirection = Vector3.zero;
    [SyncVar] private Vector3 _currentRotateOffcet = Vector3.zero;
    [SyncVar] private Quaternion _currentLookRotate = Quaternion.identity;
    [SyncVar] private bool _canMove = true;

    public string Nickname => _nickname;
    public bool IsLocalPlayer => isLocalPlayer;
    public Vector3 CurrentMoveDirection => new Vector3(_shouders.TransformDirection(_currentMoveDirection).x, 0, _shouders.TransformDirection(_currentMoveDirection).z);
    public Transform Shoulders => _shouders;
    public bool IsMoving => CurrentMoveDirection != Vector3.zero;
    public bool CanMove => _canMove;
    public Quaternion CurrentLookRotate => _currentLookRotate;
 
    #region Network Set Commands
    [Command]
    private void CommmandSetMoveDirection(Vector2 direction)
    {
        _currentMoveDirection = new Vector3(direction.x, 0, direction.y);
        ClientPRCSetMoveDirection(direction);
    }
    [ClientRpc]
    private void ClientPRCSetMoveDirection(Vector2 direction)
    {
        _currentMoveDirection = new Vector3(direction.x, 0, direction.y);
    }

    [Command]
    private void CommandSetCurrentMouseOffcet(Vector2 mouseOffcet, Quaternion shoulders)
    {
        _currentRotateOffcet = mouseOffcet;
        _currentLookRotate = shoulders;
        ClientRpcSetCurrentOffcet(mouseOffcet, shoulders);
    }
    [ClientRpc]
    private void ClientRpcSetCurrentOffcet(Vector2 mouseOffcet, Quaternion shoulders)
    {
        _currentRotateOffcet = mouseOffcet;
        _currentLookRotate = shoulders;
    }

    [Command]
    public void CommandSetNickname(string nickname)
    {
        _nickname = nickname;
        ClientRPCSetNickname(nickname);
    }
    [ClientRpc]
    private void ClientRPCSetNickname(string nickname)
    {
        _nickname = nickname;
    }
    [Command]
    public void CommandSetCanMove(bool value)
    {
        _canMove = value;
        ClientRrpcSetCanMove(value);
    }
    [ClientRpc]
    private void ClientRrpcSetCanMove(bool value)
    {
        _canMove = value;
    }
    [Command]
    private void CommandSetCurrentLookRotate()
    {
        ClientRpcSetCurrentLookRotate(_currentLookRotate);
    }
    [ClientRpc]
    private void ClientRpcSetCurrentLookRotate(Quaternion rotatinon)
    {
        Shoulders.rotation = rotatinon;
    }
    #endregion

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isClient && isLocalPlayer && CanMove)
        {
            Vector3 inputInfo = context.ReadValue<Vector2>();
            CommmandSetMoveDirection(inputInfo);
            _currentMoveDirection = new Vector3(inputInfo.x, 0, inputInfo.y);
        }
    }

    public override void OnStartClient()
    {
        if (isLocalPlayer && isClient)
        {
            CommandSetNickname(PlayerPrefs.GetString("nickname"));
        }
        CommandSetCurrentLookRotate();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if (isClient && isLocalPlayer)
        {
            Vector2 mouseOffcet = context.ReadValue<Vector2>();
            CommandSetCurrentMouseOffcet(mouseOffcet, _shouders.rotation);
            _currentRotateOffcet = mouseOffcet;
        }
    }

    private void Update()
    {
        Move(CurrentMoveDirection, Time.deltaTime);
        Rotate(_currentRotateOffcet);
    }

    private void Move(Vector3 direction, float delta)
    {   
        _charancterController.Move(new Vector3(direction.x, 0, direction.z).normalized  * _moveSpeed * delta);
    }

    private void Rotate(Vector2 offcet)
    {
        Vector3 newRotate = new Vector3(offcet.y + _shouders.transform.eulerAngles.x, offcet.x + _shouders.transform.eulerAngles.y, 0);
        for (int i = 0; i < 2; i++)
        {
            if (newRotate[i] < 180 && newRotate[i] > _maxCameraRotaion[i])
            {
                newRotate[i] = _maxCameraRotaion[i];
            }
        } 
        for (int i = 0; i < 2; i++)
        {
            if (newRotate[i] > 180 && newRotate[i] < _minCameraRotation[i])
            {
                newRotate[i] = _minCameraRotation[i];
            }
        }
        _shouders.transform.rotation = Quaternion.Euler(newRotate);
    }

    private void OnValidate()
    {
        for (int i = 0; i < 2; i++)
        {
            if (_maxCameraRotaion[i] > 180 && _maxCameraRotaion[i] < 0)
            {
                _maxCameraRotaion[i] = 180;
            }
            if (_minCameraRotation[i] < 180 && _minCameraRotation[i] < 0)
            {
                _minCameraRotation[i] = 0;
            }
        }
    }
}