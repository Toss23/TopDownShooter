using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView, IInitable
{
    [SerializeField] private TMP_Text _scoreText;

    public void PreInit(IContext context)
    {
        
    }

    public void Init(IContext context)
    {
        
    }

    public void SetScore(int score)
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + score;
        }
    }
}
