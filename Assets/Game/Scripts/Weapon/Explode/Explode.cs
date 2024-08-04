using System;

public class Explode : IUpdatable
{
    public event Action OnDestroy;

    private int _damage;
    float _duration;

    public Explode(int damage, float duration)
    {
        _damage = damage;
        _duration = duration;
    }

    public void DamageEnemy(Enemy enemy)
    {
        enemy.DealDamage(_damage);
    }

    public void UpdateGame(float deltaTime)
    {
        _duration -= deltaTime;
        if (_duration <= 0)
        {
            OnDestroy?.Invoke();
        }
    }

    public void FixedUpdateGame(float fixedDeltaTime)
    {
        
    }
}
