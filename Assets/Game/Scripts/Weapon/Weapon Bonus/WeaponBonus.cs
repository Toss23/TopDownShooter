using System;

public class WeaponBonus : IUpdatable
{
    public event Action OnDestroy;

    private WeaponVariant _weaponVariant;
    private float _lifeTimer;

    public WeaponBonus(WeaponVariant weaponVariant)
    {
        _weaponVariant = weaponVariant;
        _lifeTimer = weaponVariant.LifeTime;
    }

    public void AffectPlayer(Player player)
    {
        player.Weapon.Equip(_weaponVariant);
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
