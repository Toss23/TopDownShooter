using UnityEngine;

public class ArmouredEnemy : EnemyVariant
{
    public ArmouredEnemy() : base()
    {
        _life = 50;
        _movespeed = 2;
        _scoreReward = 30;
        _spawnWeight = 10;
        _color = Color.black;
    }
}
