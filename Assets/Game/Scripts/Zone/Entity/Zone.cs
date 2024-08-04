public abstract class Zone
{
    private float _x;
    private float _y;

    public float X => _x;
    public float Y => _y;

    public Zone(float x, float y)
    {
        _x = x;
        _y = y;
    }

    public abstract void OnStayAction(Player player);
    public abstract void OnExitAction(Player player);
}
