public class DeathZone : Zone
{
    public DeathZone(float x, float y) : base(x, y)
    {

    }

    public override void OnStayAction(Player player)
    {
        player.Kill();
    }

    public override void OnExitAction(Player player)
    {
        
    }
}
