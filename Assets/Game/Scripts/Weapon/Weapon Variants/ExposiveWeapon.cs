public class ExposiveWeapon : WeaponVariant
{
    public ExposiveWeapon() : base()
    {
        _fireRate = 0.66f;
        _bulletPerFire = 1;
        _bulletVariant = new ExposiveBullet();
    }
}
