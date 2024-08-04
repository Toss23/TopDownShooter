public interface IEnemyPresenter
{
    public Enemy Enemy { get; }

    public void Init(IContext context, Player player, ITarget target, EnemyVariant enemyVariant);
}
