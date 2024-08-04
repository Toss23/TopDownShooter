using System;
using UnityEngine;

public class Weapon
{
    public event Action<BulletVariant, float> OnShot;

    private WeaponVariant _currentWeaponVariant;
    private float _cooldownTimer;

    public WeaponVariant CurrentWeaponVariant => _currentWeaponVariant;

    public Weapon()
    {
        _currentWeaponVariant = new PistolWeapon();
    }

    public void Update(float deltaTime)
    {
        if (_currentWeaponVariant != null)
        {
            if (_cooldownTimer < 1f / _currentWeaponVariant.FireRate)
            {
                _cooldownTimer += deltaTime;
            }

            if (Input.GetMouseButton(0))
            {
                if  (_cooldownTimer >= 1f / _currentWeaponVariant.FireRate)
                {
                    _cooldownTimer = 0;

                    for (int i = 0; i < _currentWeaponVariant.BulletPerFire; i++)
                    {
                        int bulletCount = _currentWeaponVariant.BulletPerFire;
                        float deltaAngle = _currentWeaponVariant.DeltaAngleFire;
                        int bulletStartIndex = bulletCount % 2 == 0 ? -bulletCount / 2 : -(bulletCount - 1) / 2;
                        float bulletAngle = (bulletStartIndex + i) * deltaAngle;
                        Shot(bulletAngle);
                    }
                }
            }
        }
    }

    private void Shot(float deltaAngle)
    {
        OnShot?.Invoke(_currentWeaponVariant.BulletVariant, deltaAngle);
    }

    public void Equip(WeaponVariant weaponVariant)
    {
        _currentWeaponVariant = weaponVariant;
        _cooldownTimer = 0;
    }
}
