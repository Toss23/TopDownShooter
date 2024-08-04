using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreLoader : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
        if (PlayerPrefs.HasKey("Score"))
        {
            int score = PlayerPrefs.GetInt("Score");
            _scoreText.text = $"Best score: {score}";
        }
    }
}
