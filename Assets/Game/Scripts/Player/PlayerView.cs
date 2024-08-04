using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private TMP_Text _scoreText;

    public void SetScore(int score)
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + score;
        }
    }
}
