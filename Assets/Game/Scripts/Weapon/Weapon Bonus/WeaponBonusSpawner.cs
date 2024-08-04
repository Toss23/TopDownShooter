using System;
using System.Collections.Generic;

public class WeaponBonusSpawner : EntitySpawner<WeaponVariant, WeaponBonus>
{
    private const int SPAWN_INTERVAL = 10;

    private Player _player;
    private Random _random;

    public WeaponBonusSpawner() : base()
    {
        _active = false;
        _random = new Random();
        _spawnInterval = SPAWN_INTERVAL;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
        _active = true;
    }

    protected override WeaponVariant OnSpawnEntity(out float x, out float y)
    {
        List<WeaponVariant> weaponVariants = TryRemoveWeaponFromList(_entityVariants, _player.Weapon.CurrentWeaponVariant);

        int bonusIndex = _random.Next(0, weaponVariants.Count);
        x = _random.Next(-18, 18);
        y = _random.Next(-13, 13);

        return weaponVariants[bonusIndex];
    }

    private List<WeaponVariant> TryRemoveWeaponFromList(List<WeaponVariant> weaponVariants, WeaponVariant weaponVariant)
    {
        List<WeaponVariant> list = new List<WeaponVariant>();
        list.AddRange(weaponVariants);

        foreach (WeaponVariant weapon in list)
        {
            if (weapon.GetType() == weaponVariant.GetType())
            {
                list.Remove(weapon);
                return list;
            }
        }
        return weaponVariants;
    }

    protected override void OnUpdate(float deltaTime) { }
}
