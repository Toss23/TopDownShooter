using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenuView : MonoBehaviour, IDeathMenuView, IInitable
{
    public event Action OnClickRestart;
    public event Action OnClickMenu;

    [SerializeField] private GameObject _content;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    public void PreInit(IContext context)
    {
        _restartButton.onClick.AddListener(() => OnClickRestart?.Invoke());
        _menuButton.onClick.AddListener(() => OnClickMenu?.Invoke());
    }

    public void Init(IContext context)
    {
        Hide();
    }

    public void Show(string scoreText)
    {
        _scoreText.text = scoreText;
        _content.SetActive(true);
    }

    public void Hide()
    {
        _content.SetActive(false);
    }
}
