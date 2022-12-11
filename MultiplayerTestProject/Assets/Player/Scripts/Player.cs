using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{
    [SerializeField] private CharacterController _charancterController;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Vector2 _cameraMaxRotation;
    [SerializeField] private Vector2 _cameraMinRotation;

    private Vector3 _currentMoveDirection;
    private Vector3 _currentRotateOffcet;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isClient && isLocalPlayer == false)
        {
            return;
        }
        Vector3 inputInfo = context.ReadValue<Vector2>();
        CommmandSetMoveDirection(inputInfo);
        _currentMoveDirection = new Vector3(inputInfo.x, 0, inputInfo.y);
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if (isClient && isLocalPlayer == false)
        {
            return;
        }
        Vector2 mouseOffcet = context.ReadValue<Vector2>();
        CommandSetCurrentOffcet(mouseOffcet);
        _currentRotateOffcet = mouseOffcet;
    }

    [Command]
    private void CommmandSetMoveDirection(Vector2 direction)
    {
        Debug.Log(direction);
        _currentMoveDirection = new Vector3(direction.x, 0, direction.y);
    }

    [Command]
    private void CommandSetCurrentOffcet(Vector2 mouseOffcet)
    {
        Debug.Log(mouseOffcet);
        _currentRotateOffcet = mouseOffcet;
    }

    private void FixedUpdate()
    {
        Move(_currentMoveDirection);
        Rotate(_currentRotateOffcet);
    }

    private void Move(Vector3 direction)
    {
        _charancterController.Move(transform.TransformDirection(direction) * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Rotate(Vector2 offcet)
    {
        transform.Rotate(new Vector3(0, offcet.x, 0));
    }

}