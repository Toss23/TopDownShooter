using UnityEngine;

public abstract class BonusVariant
{
    protected float _lifeTime;
    protected Color _color;

    public float LifeTime => _lifeTime;
    public Color Color => _color;

    public BonusVariant() 
    {
        _lifeTime = 5;
        _color = Color.white;
    }

    public abstract void Collect(Player player);
}
