public class MovementStats
{
    private float _defaultMoveSpeed;
    private float _defaultRotationSpeed;

    public float DefaultMoveSpeed => _defaultMoveSpeed;
    public float DefaultRotationSpeed => _defaultRotationSpeed;

    public MovementStats(float defaultMoveSpeed, float defaultRotationSpeed)
    {
        _defaultMoveSpeed = defaultMoveSpeed;
        _defaultRotationSpeed = defaultRotationSpeed;
    }
}