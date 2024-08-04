using UnityEngine;

public class HasteBonus : BonusVariant
{
    public HasteBonus() : base()
    {
        _lifeTime = 5;
        _color = Color.cyan;
    }

    public override void Collect(Player player)
    {
        player.Movement.HasteEffect(10);
    }
}
