using UnityEngine;

public class LocalObject : MonoBehaviour
{
    [SerializeField] private Player _owner;
    private void Start()
    {
        DisableOtherCamera();
    }

    private void DisableOtherCamera()
    {
        if (_owner.IsLocalPlayer == false)
        {
            Destroy(gameObject);
        }
    }
}
