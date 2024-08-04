using System;

public class Enemy : IUpdatable
{
    public event Action OnDeath;

    private EnemyVariant _enemyVariant;
    private EnemyMovement _movement;
    private Player _player;

    private int _currentLife;

    public EnemyMovement Movement => _movement;

    public Enemy(EnemyVariant enemyVariant, EnemyMovement movement, Player player)
    {
        _enemyVariant = enemyVariant;
        _movement = movement;
        _player = player;
        _currentLife = enemyVariant.Life;
    }

    public void UpdateGame(float deltaTime)
    {
        _movement.UpdateRotation(deltaTime);
    }

    public void FixedUpdateGame(float fixedDeltaTime)
    {
        _movement.UpdatePosition(fixedDeltaTime);
    }

    public void CollisionWithPlayer(Player player)
    {
        player.Damage(_enemyVariant.Damage);
    }

    public void DealDamage(int damage)
    {
        if (damage > 0)
        {
            _currentLife -= damage;
            if (_currentLife <= 0)
            {
                _movement.FreezeEffect(true);
                _movement.Disable();
                _player.AddScore(_enemyVariant.ScoreReward);
                OnDeath?.Invoke();
            }
        }
    }
}
