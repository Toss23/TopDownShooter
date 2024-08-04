using System;

public class WeaponBonus : IUpdatable
{
    private const int WEAPON_LIFE_TIME = 10;

    public event Action OnDestroy;

    private WeaponVariant _weaponVariant;
    private float _lifeTimer;

    public WeaponBonus(WeaponVariant weaponVariant)
    {
        _weaponVariant = weaponVariant;
        _lifeTimer = WEAPON_LIFE_TIME;
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
