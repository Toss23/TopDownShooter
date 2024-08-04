using System;

public class EnemySpawner : EntitySpawner<EnemyVariant, Enemy>
{
    private const float SPAWN_INTERVAL = 2;
    private const float SPAWN_INTERVAL_MINIMUM = 0.5f;
    private const float DECREASE_SPAWN_INTERVAL_VALUE = 0.1f;
    private const float DECREASE_SPAWN_INTERVAL_EVERY = 10f;

    private Random _random;
    private int _sumWeight;

    private ITarget _target;

    private float _spawnIntervalMinimum;

    private float _decreaseSpawnIntervalTimer;
    private float _decreaseSpawnIntervalValue;
    private float _decreaseSpawnIntervalEvery;

    public EnemySpawner() : base()
    {
        _active = false;
        _spawnTimer = 0;
        _spawnInterval = SPAWN_INTERVAL;

        _decreaseSpawnIntervalTimer = 0;
        _decreaseSpawnIntervalValue = DECREASE_SPAWN_INTERVAL_VALUE;
        _decreaseSpawnIntervalEvery = DECREASE_SPAWN_INTERVAL_EVERY;
        _spawnIntervalMinimum = SPAWN_INTERVAL_MINIMUM;

        _random = new Random();

        foreach (EnemyVariant enemyVariant in _entityVariants)
        {
            _sumWeight += enemyVariant.SpawnWeight;
        }
    }

    public void SetTarget(ITarget target)
    {
        _target = target;
        _active = true;
    }

    protected override void OnUpdate(float deltaTime)
    {
        while (_decreaseSpawnIntervalTimer >= _decreaseSpawnIntervalEvery)
        {
            _decreaseSpawnIntervalTimer -= _decreaseSpawnIntervalEvery;
            _spawnInterval -= _decreaseSpawnIntervalValue;
            if (_spawnInterval < _spawnIntervalMinimum)
            {
                _spawnInterval = _spawnIntervalMinimum;
            }
        }
    }

    protected override EnemyVariant OnSpawnEntity(out float x, out float y)
    {
        int weight = _random.Next(0, _sumWeight);
        int prevWeight = 0;
        int nextWeight = 0;

        for (int i = 0; i < _entityVariants.Count; i++)
        {
            nextWeight = prevWeight + _entityVariants[i].SpawnWeight;
            if (weight >= prevWeight & weight < nextWeight)
            {
                float radius = 12;
                float angle = _random.Next(0, 360);
                x = (float)Math.Cos(angle) * radius + _target.Position.x;
                y = (float)Math.Sin(angle) * radius + _target.Position.y;

                return _entityVariants[i];
            }
            else
            {
                prevWeight = nextWeight;
            }
        }

        x = 0;
        y = 0;
        return null;
    }
}
