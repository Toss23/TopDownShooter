public class PistolWeapon : WeaponVariant
{
    public PistolWeapon() : base()
    {
        _fireRate = 2;
        _bulletVariant = new PistolBullet();
    }
}
