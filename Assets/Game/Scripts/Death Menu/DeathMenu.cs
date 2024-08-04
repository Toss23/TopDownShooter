using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class DeathMenu
{
    public event Action<string> OnShow;

    private int _prevScore;

    public DeathMenu()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            _prevScore = PlayerPrefs.GetInt("Score");
        }
        else
        {
            _prevScore = 0;
        }
    }

    public void Show(int score)
    {
        if (score > _prevScore)
        {
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.Save();
        }

        string scoreText = $"Score: {score}" + (score > _prevScore ? "\n<color=red>NEW RECORD!</color>" : "");

        OnShow?.Invoke(scoreText);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
