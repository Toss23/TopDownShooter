using UnityEngine;

public class ExposiveBullet : BulletVariant
{
    public ExposiveBullet() : base()
    {
        _damage = 10;
        _speed = 10;
        _explosive = true;
        _explosiveRadius = 2;
        _explodePrefab = AssetLoader.GetInstance().GetAsset<GameObject>(Assets.EXPLODE_PREFAB);
        _rangeToMouse = true;
        _destroyOnHit = false;
    }
}
