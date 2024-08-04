using UnityEngine;

public class BulletMovement : Movement
{
    public BulletMovement(IContext context, Rigidbody2D rigidbody, MovementStats movementStats) : base(context, rigidbody, movementStats)
    {
        
    }

    public override void UpdatePosition(float fixedDeltaTime)
    {
        _rigidbody.velocity = _movementStats.DefaultMoveSpeed * _rigidbody.transform.right;
    }

    public override void UpdateRotation(float deltaTime)
    {
        
    }
}
