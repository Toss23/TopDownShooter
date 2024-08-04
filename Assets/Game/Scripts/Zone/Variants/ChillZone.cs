public class ChillZone : Zone
{
    public ChillZone(float x, float y) : base(x, y)
    {

    }

    public override void OnStayAction(Player player)
    {
        player.Movement.ChillEffect(true);
    }

    public override void OnExitAction(Player player)
    {
        player.Movement.ChillEffect(false);
    }
}
