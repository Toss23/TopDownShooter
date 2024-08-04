using UnityEngine;

public class BonusPresenter : MonoBehaviour, IBonusPresenter
{
    [SerializeField] private SpriteRenderer _sprite;

    private Bonus _bonus;
    private Player _player;
    private IContext _context;

    public void Init(IContext context, BonusVariant bonusVariant, Player player)
    {
        _context = context;
        _sprite.color = bonusVariant.Color;
        _bonus = new Bonus(bonusVariant);
        _player = player;
        Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _bonus.AffectPlayer(_player);
        }
    }

    public void Enable()
    {
        _context.AddUpdatable(_bonus);
        _bonus.OnDestroy += DestroyBonus;
    }

    public void Disable()
    {
        _context.RemoveUpdatable(_bonus);
        _bonus.OnDestroy -= DestroyBonus;
    }

    private void DestroyBonus()
    {
        Disable();
        Destroy(gameObject);
    }
}
