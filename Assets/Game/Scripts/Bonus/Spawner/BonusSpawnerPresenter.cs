using UnityEngine;

public class BonusSpawnerPresenter : MonoBehaviour, IInitable
{
    private BonusSpawner _bonusSpawner;
    private AssetLoader _assetLoader;
    private GameObject _bonusPrefab;
    private Player _player;
    private IContext _context;

    public void PreInit(IContext context)
    {
        _context = context;
        _bonusSpawner = new BonusSpawner();
        context.AddUpdatable(_bonusSpawner);
    }

    public void Init(IContext context)
    {
        _assetLoader = AssetLoader.GetInstance();
        _bonusPrefab = _assetLoader.GetAsset<GameObject>(Assets.BONUS_PREFAB);
        _player = context.GetObjectWithInterface<IPlayerPresenter>(typeof(PlayerPresenter)).Player;
        Enable();
    }

    public void Enable()
    {
        _bonusSpawner.OnSpawn += SpawnBonus;
    }

    public void Disable()
    {
        _bonusSpawner.OnSpawn -= SpawnBonus;
    }

    private void SpawnBonus(BonusVariant bonusVariant, float x, float y)
    {
        GameObject bonusGameObject = Instantiate(_bonusPrefab, transform);
        bonusGameObject.name = bonusVariant.ToString();
        bonusGameObject.transform.position = new Vector2(x, y);

        IBonusPresenter bonusPresenter = bonusGameObject.GetComponent<IBonusPresenter>();
        bonusPresenter.Init(_context, bonusVariant, _player);
    }
}
