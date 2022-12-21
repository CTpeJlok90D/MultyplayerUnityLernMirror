using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTabView : MonoBehaviour
{
    [SerializeField] private PlayersTabViewItem _itemPrefab;
    [SerializeField] private Transform group;

    private void OnEnable()
    {
        foreach (Transform gameObject in transform)
        {
            Destroy(gameObject.gameObject);
        }
        foreach (Player player in Player.List)
        {
            Instantiate(_itemPrefab, group).Init(player).transform.SetParent(group);
        }
    }

    public void TogleView(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            gameObject.SetActive(true);
        }
        if (context.canceled)
        {
            gameObject.SetActive(false);
        }
    }
}
