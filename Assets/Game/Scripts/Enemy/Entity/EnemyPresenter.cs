using UnityEngine;

public class EnemyPresenter : MonoBehaviour, IEnemyPresenter
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Enemy _enemy;
    private Player _player;
    private IContext _context;

    public Enemy Enemy => _enemy;

    public void Init(IContext context, Player player, ITarget target, EnemyVariant enemyVariant)
    {
        _player = player;
        _context = context;
        _sprite.color = enemyVariant.Color;
        MovementStats movementStats = new MovementStats(enemyVariant.MoveSpeed, 720);
        _enemy = new Enemy(enemyVariant, new EnemyMovement(context, _rigidbody, movementStats, target), player);

        Enable();
    }

    private void Enable()
    {
        _context.AddUpdatable(_enemy);
        _enemy.OnDeath += OnDeath;
    }

    private void Disable()
    {
        _context.RemoveUpdatable(_enemy);
        _enemy.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        Disable();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemy.CollisionWithPlayer(_player);
        }
    }
}
