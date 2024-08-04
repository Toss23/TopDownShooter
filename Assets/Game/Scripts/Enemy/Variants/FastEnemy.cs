using UnityEngine;

public class FastEnemy : EnemyVariant
{
    public FastEnemy() : base()
    {
        _life = 10;
        _movespeed = 4;
        _scoreReward = 12;
        _spawnWeight = 30;
        _color = Color.green;
    }
}
