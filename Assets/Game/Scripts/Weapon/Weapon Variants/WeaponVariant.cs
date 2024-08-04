public abstract class WeaponVariant
{
    protected float _fireRate;
    protected int _bulletPerFire;
    protected float _deltaAngleFire;
    protected BulletVariant _bulletVariant;

    public float FireRate => _fireRate;
    public int BulletPerFire => _bulletPerFire;
    public float DeltaAngleFire => _deltaAngleFire;
    public BulletVariant BulletVariant => _bulletVariant;

    public WeaponVariant()
    {
        _fireRate = 0;
        _bulletPerFire = 0;
        _deltaAngleFire = 0;
        _bulletVariant = new PistolBullet();
    }
}
