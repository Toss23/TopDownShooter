using UnityEngine;

public class BaseEnemy : EnemyVariant
{
    public BaseEnemy() : base()
    {
        _life = 10;
        _movespeed = 3;
        _scoreReward = 7;
        _spawnWeight = 60;
        _color = Color.white;
    }
}
