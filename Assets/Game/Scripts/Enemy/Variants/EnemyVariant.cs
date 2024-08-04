using UnityEngine;

public abstract class EnemyVariant
{
    protected int _life;
    protected int _movespeed;
    protected int _damage;
    protected int _scoreReward;
    protected int _spawnWeight;
    protected Color _color;

    public int Life => _life;
    public int MoveSpeed => _movespeed;
    public int Damage => _damage;
    public int ScoreReward => _scoreReward;
    public int SpawnWeight => _spawnWeight;
    public Color Color => _color;

    protected EnemyVariant()
    {
        _life = 1;
        _movespeed = 1;
        _damage = 100;
        _scoreReward = 0;
        _spawnWeight = 0;
        _color = Color.white;
    }
}