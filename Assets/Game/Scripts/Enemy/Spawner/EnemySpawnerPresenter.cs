using UnityEngine;

public class EnemySpawnerPresenter : MonoBehaviour, IEnemySpawnerPresenter, IInitable
{
    private EnemySpawner _enemySpawner;

    private AssetLoader _assetLoader;
    private GameObject _enemyPrefab;
    private IContext _context;
    private ITarget _target;
    private Player _player;

    public EnemySpawner EnemySpawner => _enemySpawner;

    public void PreInit(IContext context)
    {
        _context = context;
        _enemySpawner = new EnemySpawner();
        context.AddUpdatable(_enemySpawner);
    }

    public void Init(IContext context)
    {
        _assetLoader = AssetLoader.GetInstance();
        _enemyPrefab = _assetLoader.GetAsset<GameObject>(Assets.ENEMY_PREFAB);

        _player = context.GetObjectWithInterface<IPlayerPresenter>(typeof(PlayerPresenter)).Player;
        _target = _player.Movement;
        _enemySpawner.SetTarget(_target);

        Enable();
    }

    public void Enable()
    {
        _enemySpawner.OnSpawn += SpawnEnemy;
    }

    public void Disable()
    {
        _enemySpawner.OnSpawn -= SpawnEnemy;
    }

    private void SpawnEnemy(EnemyVariant enemyVariant, float x, float y)
    {
        GameObject enemyGameObject = Instantiate(_enemyPrefab, transform);
        enemyGameObject.name = enemyVariant.ToString();
        enemyGameObject.transform.position = new Vector2(x, y);

        IEnemyPresenter enemyPresenter = enemyGameObject.GetComponent<IEnemyPresenter>();
        enemyPresenter.Init(_context, _player, _target, enemyVariant);
    }
}
