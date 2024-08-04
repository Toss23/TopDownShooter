using UnityEngine;

public abstract class Movement : ITarget
{
    protected MovementStats _movementStats;

    protected IContext _context;
    protected Rigidbody2D _rigidbody;
    protected float _movespeed;
    protected bool _isFreezed;

    public Vector2 Position { get { return _rigidbody.position; } }
    public Vector2 Right { get { return _rigidbody.transform.right; } }
    public float Rotation { get { return _rigidbody.rotation; } }

    public Movement(IContext context, Rigidbody2D rigidbody, MovementStats movementStats)
    {
        _context = context;
        _rigidbody = rigidbody;
        _isFreezed = false;
        _movementStats = movementStats;
        _movespeed = _movementStats.DefaultMoveSpeed;

        Enable();
    }

    public void Enable()
    {
        _context.OnPause += FreezeEffect;
    }

    public void Disable()
    {
        _context.OnPause -= FreezeEffect;
    }

    public void ChillEffect(bool active)
    {
        _movespeed = _movementStats.DefaultMoveSpeed * (active ? 0.6f : 1);
    }

    public void FreezeEffect(bool active)
    {
        _isFreezed = active;
        _rigidbody.velocity = Vector2.zero;
    }

    public abstract void UpdateRotation(float deltaTime);
    public abstract void UpdatePosition(float fixedDeltaTime);
}