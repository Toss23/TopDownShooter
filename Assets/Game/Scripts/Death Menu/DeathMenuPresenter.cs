using UnityEngine;

[RequireComponent(typeof(DeathMenuView))]
public class DeathMenuPresenter : MonoBehaviour, IDeathMenuPresenter, IInitable
{
    private IDeathMenuView _deathMenuView;
    private DeathMenu _deathMenu;

    public DeathMenu DeathMenu => _deathMenu;

    public void PreInit(IContext context)
    {
        _deathMenuView = GetComponent<IDeathMenuView>();
        _deathMenu = new DeathMenu();
    }

    public void Init(IContext context)
    {
        Enable();
    }

    public void Enable()
    {
        _deathMenuView.OnClickRestart += _deathMenu.RestartGame;
        _deathMenuView.OnClickMenu += _deathMenu.LoadMenu;
        _deathMenu.OnShow += _deathMenuView.Show;
    }

    public void Disable()
    {
        _deathMenuView.OnClickRestart -= _deathMenu.RestartGame;
        _deathMenuView.OnClickMenu -= _deathMenu.LoadMenu;
        _deathMenu.OnShow -= _deathMenuView.Show;
    }
}
