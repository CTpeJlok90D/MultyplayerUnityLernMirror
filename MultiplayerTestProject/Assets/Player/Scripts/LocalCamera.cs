using UnityEngine;

public class LocalCamera : MonoBehaviour
{
    [SerializeField] private Player _owner;
    private void Start()
    {
        transform.SetParent(null);
        DisableOtherCamera();
    }

    private void DisableOtherCamera()
    {
        if (_owner.IsLocalPlayer == false)
        {
            Destroy(this.gameObject);
        }
    }
}
