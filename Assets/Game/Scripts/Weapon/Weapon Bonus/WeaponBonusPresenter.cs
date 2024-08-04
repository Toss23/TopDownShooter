using TMPro;
using UnityEngine;

public class WeaponBonusPresenter : MonoBehaviour, IWeaponBonusPresenter
{
    [SerializeField] private TMP_Text _nameText;

    private WeaponBonus _weaponBonus;
    private Player _player;
    private IContext _context;

    public void Init(IContext context, WeaponVariant weaponVariant, Player player)
    {
        _nameText.text = weaponVariant.ToString()[0].ToString();
        _context = context;
        _weaponBonus = new WeaponBonus(weaponVariant);
        _player = player;
        Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _weaponBonus.AffectPlayer(_player);
        }
    }

    public void Enable()
    {
        _context.AddUpdatable(_weaponBonus);
        _weaponBonus.OnDestroy += DestroyBonus;
    }

    public void Disable()
    {
        _context.RemoveUpdatable(_weaponBonus);
        _weaponBonus.OnDestroy -= DestroyBonus;
    }

    private void DestroyBonus()
    {
        Disable();
        Destroy(gameObject);
    }
}
