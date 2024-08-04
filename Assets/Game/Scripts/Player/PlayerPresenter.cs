using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerView))]
public class PlayerPresenter : MonoBehaviour, IPlayerPresenter, IInitable
{
    private const int MOVE_SPEED = 4;
    private const int ROTATION_SPEED = 180;

    private IPlayerView _playerView;
    private Player _player;
    private IContext _context;
    private AssetLoader _assetLoader;
    private GameObject _bulletPrefab;

    private BulletPool _bulletPool;

    public Player Player => _player;

    public void PreInit(IContext context)
    {
        _playerView = GetComponent<IPlayerView>();
        _context = context;
        _assetLoader = AssetLoader.GetInstance();
        _bulletPrefab = _assetLoader.GetAsset<GameObject>(Assets.BULLET_PREFAB);

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        _player = new Player(new PlayerMovement(context, rigidbody, new MovementStats(MOVE_SPEED, ROTATION_SPEED)));
        _bulletPool = new BulletPool(_player);
    }

    public void Init(IContext context)
    {
        Enable();
    }

    public void Enable()
    {
        _context.AddUpdatable(_player);
        _player.OnAddScore += _playerView.SetScore;
        _player.OnDeath += _context.OnPlayerDeath;
        _player.Weapon.OnShot += _bulletPool.SpawnBullet;
        _bulletPool.RequireInstatiate += InstatiateBullet;
    }

    public void Disable()
    {
        _context.RemoveUpdatable(_player);
        _player.OnAddScore -= _playerView.SetScore;
        _player.OnDeath -= _context.OnPlayerDeath;
        _bulletPool.RequireInstatiate -= InstatiateBullet;
    }

    private void InstatiateBullet(BulletVariant bulletVariant, float deltaAngle)
    {
        Vector2 position = transform.position + transform.right;
        Quaternion quaternion = Quaternion.Euler(0, 0, _player.Movement.Rotation + deltaAngle);

        GameObject bulletGameObject = Instantiate(_bulletPrefab, position, quaternion);
        bulletGameObject.name = bulletVariant.ToString();

        if (bulletVariant.Explosive == true)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float range = Vector2.Distance(_player.Movement.Position, mousePosition);
            bulletVariant = bulletVariant.GetCopy();
            bulletVariant.SetRange(range - bulletVariant.ExplosiveRadius / 2);
        }

        IBulletPresenter bulletPresenter = bulletGameObject.GetComponent<IBulletPresenter>();
        if (bulletPresenter != null)
        {
            bulletPresenter.Init(_context, bulletVariant);
            _bulletPool.AddToPool(bulletPresenter);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IZonePresenter zonePresenter = IsZone(collision);
        zonePresenter?.Zone.OnStayAction(_player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IZonePresenter zonePresenter = IsZone(collision);
        zonePresenter?.Zone.OnExitAction(_player);
    }

    private IZonePresenter IsZone(Collider2D collision)
    {
        if (collision.CompareTag("Zone"))
        {
            IZonePresenter zonePresenter = collision.GetComponent<IZonePresenter>();
            return zonePresenter;
        }
        return null;
    }
}
