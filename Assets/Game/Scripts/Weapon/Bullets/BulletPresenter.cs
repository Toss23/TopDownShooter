using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletPresenter : MonoBehaviour, IBulletPresenter
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private BulletVariant _bulletVariant;
    private Bullet _bullet;
    private IContext _context;

    public BulletVariant BulletVariant => _bulletVariant;
    public bool IsActive => gameObject.activeSelf;

    public void Init(IContext context, BulletVariant bulletVariant)
    {
        _context = context;
        _bulletVariant = bulletVariant;
        _bullet = new Bullet(bulletVariant, new BulletMovement(context, _rigidbody, new MovementStats(bulletVariant.Speed, 0)));
        context.AddUpdatable(_bullet);

        Enable();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _context.AddUpdatable(_bullet);
        _bullet.OnDestroy += DestroyBullet;

        if (_bulletVariant.Explosive == true)
        {
            _bullet.OnEndRange += Explode;
        }
    }

    public void Disable()
    {
        _context.RemoveUpdatable(_bullet);
        _bullet.OnDestroy -= DestroyBullet;

        if (_bulletVariant.Explosive == true)
        {
            _bullet.OnEndRange -= Explode;
        }

        gameObject.SetActive(false);
    }

    public void Reuse(Vector2 position, Quaternion quaternion, float range)
    {
        transform.position = position;
        transform.rotation = quaternion;

        if (_bulletVariant.Explosive == true)
        {
            _bulletVariant = _bulletVariant.GetCopy();
            _bulletVariant.SetRange(range);
        }

        _bullet.Reuse(_bulletVariant.Range);

        Enable();
    }

    private void DestroyBullet()
    {
        Disable();
    }

    public void Explode()
    {
        Explode(_rigidbody.position.x, _rigidbody.position.y, _bulletVariant.ExplosiveRadius);
    }

    public void Explode(float x, float y, float radius)
    {
        GameObject explodePrefab = Instantiate(_bulletVariant.ExplosivePrefab, new Vector2(x, y), Quaternion.identity);
        explodePrefab.transform.localScale = Vector3.one * radius * 2;
        explodePrefab.name = "Explode";

        IExplodePresenter explodePresenter = explodePrefab.GetComponent<IExplodePresenter>();
        explodePresenter.Init(_context, _bulletVariant.Damage, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bulletVariant.Explosive == false & collision.CompareTag("Enemy"))
        {
            IEnemyPresenter enemyPresenter = collision.GetComponent<IEnemyPresenter>();
            if (enemyPresenter != null)
            {
                _bullet.OnHitEnemy(enemyPresenter.Enemy);
            }
        }
    }
}
