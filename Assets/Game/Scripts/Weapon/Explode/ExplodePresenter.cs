using UnityEngine;

public class ExplodePresenter : MonoBehaviour, IExplodePresenter
{
    private IContext _context;
    private Explode _explode;

    public void Init(IContext context, int damage, float duration)
    {
        _context = context;
        _explode = new Explode(damage, duration);
        Enable();
    }

    public void Enable()
    {
        _context.AddUpdatable(_explode);
        _explode.OnDestroy += DestroyExlode;
    }

    public void Disable()
    {
        _context.RemoveUpdatable(_explode);
        _explode.OnDestroy -= DestroyExlode;
    }

    private void DestroyExlode()
    {
        Disable();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<IEnemyPresenter>().Enemy;
            _explode.DamageEnemy(enemy);
        }
    }
}
