using System;
using UnityEngine;

public abstract class BulletVariant
{
    protected int _damage;
    protected float _range;
    protected int _speed;
    protected bool _explosive;
    protected float _explosiveRadius;
    protected GameObject _explodePrefab;
    protected bool _rangeToMouse;
    protected bool _destroyOnHit;

    public int Damage => _damage;
    public float Range => _range;
    public int Speed => _speed;
    public bool Explosive => _explosive;
    public float ExplosiveRadius => _explosiveRadius;
    public GameObject ExplosivePrefab => _explodePrefab;
    public bool RangeToMouse => _rangeToMouse;
    public bool DestroyOnHit => _destroyOnHit;

    public BulletVariant()
    {
        _damage = 1;
        _range = 100;
        _speed = 30;
        _explosive = false;
        _explosiveRadius = 0;
        _explodePrefab = null;
        _rangeToMouse = false;
        _destroyOnHit = true;
    }

    public void SetRange(float range)
    {
        _range = range;
    }

    public BulletVariant GetCopy()
    {
        BulletVariant bulletVariant = (BulletVariant)Activator.CreateInstance(this.GetType());
        bulletVariant._damage = _damage;
        bulletVariant._range = _range;
        bulletVariant._speed = _speed;
        bulletVariant._explosive = _explosive;
        bulletVariant._explosiveRadius = _explosiveRadius;
        bulletVariant._explodePrefab = _explodePrefab;
        bulletVariant._rangeToMouse = _rangeToMouse;
        bulletVariant._destroyOnHit = _destroyOnHit;
        return bulletVariant;
    }
}
