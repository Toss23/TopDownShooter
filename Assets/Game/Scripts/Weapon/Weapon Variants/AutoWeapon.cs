public class AutoWeapon : WeaponVariant
{
    public AutoWeapon() : base()
    {
        _fireRate = 10;
        _bulletPerFire = 1;
        _deltaAngleFire = 0;
        _bulletVariant = new AutoBullet();
    }
}
