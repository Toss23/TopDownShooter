public abstract class WeaponVariant
{
    protected float _lifeTime;
    protected float _fireRate;
    protected int _bulletPerFire;
    protected float _deltaAngleFire;
    protected BulletVariant _bulletVariant;

    public float LifeTime => _lifeTime;
    public float FireRate => _fireRate;
    public int BulletPerFire => _bulletPerFire;
    public float DeltaAngleFire => _deltaAngleFire;
    public BulletVariant BulletVariant => _bulletVariant;

    public WeaponVariant()
    {
        _lifeTime = 5;
        _fireRate = 1;
        _bulletPerFire = 1;
        _deltaAngleFire = 0;
        _bulletVariant = new PistolBullet();
    }
}
