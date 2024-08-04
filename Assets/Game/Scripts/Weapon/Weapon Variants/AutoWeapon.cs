public class AutoWeapon : WeaponVariant
{
    public AutoWeapon() : base()
    {
        _fireRate = 10;
        _bulletVariant = new AutoBullet();
    }
}
