using UnityEngine;

public class ImmuneBonus : BonusVariant
{
    public ImmuneBonus() : base()
    {
        _lifeTime = 5;
        _color = Color.yellow;
    }

    public override void Collect(Player player)
    {
        player.ImmuneEffect(10);
    }
}
