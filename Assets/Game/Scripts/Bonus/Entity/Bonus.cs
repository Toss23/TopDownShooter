using System;

public class Bonus : IUpdatable
{
    public event Action OnDestroy;

    private BonusVariant _bonusVariant;
    private float _lifeTimer;

    public Bonus(BonusVariant bonusVariant)
    {
        _bonusVariant = bonusVariant;
        _lifeTimer = bonusVariant.LifeTime;
    }

    public void AffectPlayer(Player player)
    {
        _bonusVariant.Collect(player);
        OnDestroy?.Invoke();
    }

    public void UpdateGame(float deltaTime)
    {
        _lifeTimer -= deltaTime;
        if (_lifeTimer <= 0)
        {
            OnDestroy?.Invoke();
        }
    }

    public void FixedUpdateGame(float fixedDeltaTime)
    {
        
    }
}
