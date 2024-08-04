using System;

public class BonusSpawner : EntitySpawner<BonusVariant, Bonus>
{
    private const int SPAWN_INTERVAL = 27;

    private Random _random;

    public BonusSpawner() : base()
    {
        _random = new Random();
        _spawnInterval = SPAWN_INTERVAL;
    }

    protected override BonusVariant OnSpawnEntity(out float x, out float y)
    {
        int bonusIndex = _random.Next(0, _entityVariants.Count);
        x = _random.Next(-18, 18);
        y = _random.Next(-13, 13);
        return _entityVariants[bonusIndex];
    }

    protected override void OnUpdate(float deltaTime) { }
}
