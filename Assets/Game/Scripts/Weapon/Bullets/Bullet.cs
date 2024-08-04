using System;

public class Bullet : IUpdatable
{
    public event Action OnDestroy;
    public event Action OnEndRange;

    private BulletVariant _bulletVariant;
    private BulletMovement _movement;

    private float _lifeTimer;

    public BulletMovement Movement => _movement;

    public Bullet(BulletVariant bulletVariant, BulletMovement bulletMovement)
    {
        _bulletVariant = bulletVariant;
        _movement = bulletMovement;
        _lifeTimer = _bulletVariant.Range;
    }

    public void OnHitEnemy(Enemy enemy) 
    {
        enemy.DealDamage(_bulletVariant.Damage);

        if (_bulletVariant.DestroyOnHit == true)
        {
            Destroy();
        }
    }

    public void UpdateGame(float deltaTime)
    {
        _lifeTimer -= _bulletVariant.Speed * deltaTime;
        if (_lifeTimer <= 0)
        {
            OnEndRange?.Invoke();
            Destroy();
        }
    }

    private void Destroy()
    {
        _movement.FreezeEffect(true);
        _movement.Disable();
        OnDestroy?.Invoke();
    }

    public void Reuse(float range)
    {
        _lifeTimer = range;
    }

    public void FixedUpdateGame(float fixedDeltaTime)
    {
        _movement.UpdatePosition(fixedDeltaTime);
    }
}
