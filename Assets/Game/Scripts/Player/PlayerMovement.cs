using UnityEngine;

public class PlayerMovement : Movement
{
    private bool _isHasted;
    private float _hasteTimer;

    public PlayerMovement(IContext context, Rigidbody2D rigidbody, MovementStats movementStats) : base(context, rigidbody, movementStats) 
    {
        _isHasted = false;
    }

    public void HasteEffect(float duration)
    {
        _isHasted = true;
        _hasteTimer = duration;
    }

    public override void UpdateRotation(float deltaTime)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = mousePosition - _rigidbody.transform.position;

        float angelBetween = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        Quaternion rotateTo = Quaternion.Euler(0, 0, angelBetween);

        if (_isFreezed == false)
        {
            _rigidbody.transform.rotation = Quaternion.RotateTowards(_rigidbody.transform.rotation, rotateTo, _movementStats.DefaultRotationSpeed * deltaTime);
        }
    }

    public override void UpdatePosition(float fixedDeltaTime)
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow) & Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection = new Vector2(moveDirection.x, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection = new Vector2(moveDirection.x, 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection = new Vector2(moveDirection.x, -1);
        }
        else
        {
            moveDirection = new Vector2(moveDirection.x, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) & Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = new Vector2(0, moveDirection.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = new Vector2(1, moveDirection.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = new Vector2(-1, moveDirection.y);
        }
        else
        {
            moveDirection = new Vector2(0, moveDirection.y);
        }

        _rigidbody.velocity = moveDirection.normalized * _movespeed;
        
        if (_isHasted == true)
        {
            _rigidbody.velocity = _rigidbody.velocity * 1.5f;
            _hasteTimer -= fixedDeltaTime;

            if (_hasteTimer <= 0)
            {
                _hasteTimer = 0;
                _isHasted = false;
            }
        }

        if (_isFreezed == true)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
