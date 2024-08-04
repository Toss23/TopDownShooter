public class PistolWeapon : WeaponVariant
{
    public PistolWeapon() : base()
    {
        _fireRate = 2;
        _bulletPerFire = 1;
        _deltaAngleFire = 0;
        _bulletVariant = new PistolBullet();
    }
}
