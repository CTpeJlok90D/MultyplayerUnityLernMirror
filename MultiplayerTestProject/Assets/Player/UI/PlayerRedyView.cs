using TMPro;
using UnityEngine;

public class PlayerRedyView : MonoBehaviour
{
    [SerializeField] private PlayerReady _playerReady;
    [SerializeField] private TMP_Text _text;
    [TextArea(1, 3)][SerializeField] private string _readyText = "You're ready";
    [TextArea(1, 3)][SerializeField] private string _notReadyText = "You're not ready";

    private void OnEnable()
    {
        _playerReady.ValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _playerReady.ValueChanged.RemoveListener(OnValueChanged);
    }

    private void Awake()
    {
        OnValueChanged();
    }

    private void OnValueChanged() 
    {
        _text.text = _playerReady.State ? _readyText : _notReadyText;
    }
}
