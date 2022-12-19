using UnityEngine;
using UnityEngine.InputSystem;

public interface IAbility
{
    public void Use(InputAction.CallbackContext context);
}
