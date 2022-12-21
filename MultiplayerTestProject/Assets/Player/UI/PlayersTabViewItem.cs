using TMPro;
using UnityEngine;

internal class PlayersTabViewItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _nicknameField;

    private Player _owner;

    public TMP_Text TMP_Text => _nicknameField;

    public PlayersTabViewItem Init(Player owner)
    {
        _owner = owner;
        Player.SomeDisconected.AddListener(OnSomeDisconected);
        _nicknameField.text = owner.Nickname;
        return this;
    }

    private void OnSomeDisconected(Player disconectedPlayer)
    {
        if (_owner == disconectedPlayer)
        {
            Destroy(gameObject);
        }
    }
}