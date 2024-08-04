using UnityEngine;

public class EnemyMovement : Movement
{
    private ITarget _target;

    public EnemyMovement(IContext context, Rigidbody2D rigidbody, MovementStats movementStats, ITarget target) : base(context, rigidbody, movementStats)
    {
        _target = target;
    }

    public override void UpdatePosition(float fixedDeltaTime)
    {
        if (_isFreezed == false & Vector2.Distance(_rigidbody.position, _target.Position) >= 0.5f)
        {
            _rigidbody.velocity = _rigidbody.transform.right * _movespeed;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    public override void UpdateRotation(float deltaTime)
    {
        Vector2 deltaPosition = _target.Position - _rigidbody.position;
        float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;

        Quaternion enemyRotation = Quaternion.Euler(0, 0, _rigidbody.rotation);
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        _rigidbody.transform.rotation = Quaternion.RotateTowards(enemyRotation, targetRotation, _movementStats.DefaultRotationSpeed);
    }
}
