using UnityEngine;

public interface IBulletPresenter
{
    public BulletVariant BulletVariant { get; }
    public bool IsActive { get; }

    public void Init(IContext context, BulletVariant bulletVariant);
    public void Enable();
    public void Disable();
    public void Reuse(Vector2 position, Quaternion quaternion, float range);
}
