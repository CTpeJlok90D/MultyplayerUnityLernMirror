using TMPro;
using UnityEngine;

public class LocalPlayerInfoContainer : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nicknameField;

    public void OnNicknameChanged(string newName)
    {
        PlayerPrefs.SetString("nickname", newName);
    }

    private void Awake()
    {
        if (PlayerPrefs.GetString("nickname") == string.Empty)
        {
            PlayerPrefs.SetString("nickname", "Player");
        }
        _nicknameField.text = PlayerPrefs.GetString("nickname");
        _nicknameField.onEndEdit.AddListener(OnNicknameChanged);
    }
}