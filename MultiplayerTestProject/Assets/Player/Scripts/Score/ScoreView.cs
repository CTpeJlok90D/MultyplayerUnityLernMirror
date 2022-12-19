using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        UpdateView(_score.Current);
        _score.CurrentChanged.AddListener(UpdateView);
    }

    private void UpdateView(int value)
    {
        _scoreText.text = value.ToString();
    }
}
