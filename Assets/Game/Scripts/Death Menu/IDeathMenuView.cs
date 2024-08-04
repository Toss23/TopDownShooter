using System;

public interface IDeathMenuView
{
    public event Action OnClickRestart;
    public event Action OnClickMenu;

    public void Show(string scoreText);
    public void Hide();
}
