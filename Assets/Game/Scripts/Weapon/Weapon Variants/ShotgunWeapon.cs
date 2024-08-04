public class ShotgunWeapon : WeaponVariant
{
    public ShotgunWeapon() : base()
    {
        _fireRate = 1.5f;
        _bulletPerFire = 5;
        _deltaAngleFire = 10;
        _bulletVariant = new ShotgunBullet();
    }
}
