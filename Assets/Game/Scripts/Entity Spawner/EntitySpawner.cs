using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public abstract class EntitySpawner<Variant, Entity> : IUpdatable
{
    public event Action<Variant, float, float> OnSpawn;

    protected bool _active;
    protected float _spawnTimer;
    protected float _spawnInterval;

    protected List<Variant> _entityVariants;

    public EntitySpawner()
    {
        _active = true;
        _spawnTimer = 0;
        _spawnInterval = 10;
        _entityVariants = FindAllVariants();
    }

    private List<Variant> FindAllVariants()
    {
        List<Variant> list = new List<Variant>();
        foreach (Type type in Assembly.GetAssembly(typeof(Variant)).GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Variant))))
        {
            list.Add((Variant)Activator.CreateInstance(type));
        }
        return list;
    }

    public void UpdateGame(float deltaTime)
    {
        if (_active == true)
        {
            _spawnTimer += deltaTime;

            while (_spawnTimer >= _spawnInterval)
            {
                _spawnTimer -= _spawnInterval;

                float x = 0;
                float y = 0;
                Variant variant = OnSpawnEntity(out x, out y);
                if (variant != null)
                {
                    OnSpawn?.Invoke(variant, x, y);
                }
            }

            OnUpdate(deltaTime);
        }
    }

    public void FixedUpdateGame(float fixedDeltaTime) { }

    protected abstract void OnUpdate(float deltaTime);
    protected abstract Variant OnSpawnEntity(out float x, out float y);
}
