using UnityEngine;

public class WeaponBonusSpawnerPresenter : MonoBehaviour, IInitable
{
    private WeaponBonusSpawner _weaponBonusSpawner;
    private AssetLoader _assetLoader;
    private GameObject _weaponBonusPrefab;
    private Player _player;
    private IContext _context;

    public void PreInit(IContext context)
    {
        _context = context;
        _weaponBonusSpawner = new WeaponBonusSpawner();
    }

    public void Init(IContext context)
    {
        _assetLoader = AssetLoader.GetInstance();
        _weaponBonusPrefab = _assetLoader.GetAsset<GameObject>(Assets.WEAPON_BONUS_PREFAB);
        _player = context.GetObjectWithInterface<IPlayerPresenter>(typeof(PlayerPresenter)).Player;
        _weaponBonusSpawner.SetPlayer(_player);
        Enable();
    }

    public void Enable()
    {
        _context.AddUpdatable(_weaponBonusSpawner);
        _weaponBonusSpawner.OnSpawn += SpawnBonus;
    }

    public void Disable()
    {
        _context.RemoveUpdatable(_weaponBonusSpawner);
        _weaponBonusSpawner.OnSpawn -= SpawnBonus;
    }

    private void SpawnBonus(WeaponVariant weaponBonusVariant, float x, float y)
    {
        GameObject weaponBonusGameObject = Instantiate(_weaponBonusPrefab, transform);
        weaponBonusGameObject.name = weaponBonusVariant.ToString();
        weaponBonusGameObject.transform.position = new Vector2(x, y);

        IWeaponBonusPresenter weaponBonusPresenter = weaponBonusGameObject.GetComponent<IWeaponBonusPresenter>();
        weaponBonusPresenter.Init(_context, weaponBonusVariant, _player);
    }
}
