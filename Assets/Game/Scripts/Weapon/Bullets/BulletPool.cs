using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    public event Action<BulletVariant, float> RequireInstatiate;

    private List<IBulletPresenter> _bullets;
    private Player _player;
    
    public BulletPool(Player player)
    {
        _bullets = new List<IBulletPresenter>();
        _player = player;
    }

    public void SpawnBullet(BulletVariant bulletVariant, float deltaAngle)
    {
        bool _spawned = false;
        foreach (IBulletPresenter bullet in _bullets)
        {
            if (bullet.IsActive == false & bulletVariant.GetType() == bullet.BulletVariant.GetType())
            {
                Vector2 position = _player.Movement.Position + _player.Movement.Right;
                Quaternion quaternion = Quaternion.Euler(0, 0, _player.Movement.Rotation + deltaAngle);

                if (bulletVariant.Explosive == true)
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    float range = Vector2.Distance(_player.Movement.Position, mousePosition);
                    bulletVariant = bulletVariant.GetCopy();
                    bulletVariant.SetRange(range - bulletVariant.ExplosiveRadius / 2);
                }

                bullet.Reuse(position, quaternion, bulletVariant.Range);
                _spawned = true;
                break;
            }
        }

        if (_spawned == false)
        {
            RequireInstatiate?.Invoke(bulletVariant, deltaAngle);
        }
    }

    public void AddToPool(IBulletPresenter bullet)
    {
        _bullets.Add(bullet);
    }
}