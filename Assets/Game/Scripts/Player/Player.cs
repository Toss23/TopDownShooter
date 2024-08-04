using System;

public class Player : IUpdatable
{
    private const int START_LIFE = 100;
    
    public event Action OnDeath;
    public event Action<int> OnAddScore;

    private int _life;
    private int _score;

    private PlayerMovement _movement;
    private Weapon _weapon;

    private bool _immune;
    private float _immuneTimer;

    public int Score => _score;
    public PlayerMovement Movement => _movement;
    public Weapon Weapon => _weapon;

    public Player(PlayerMovement movement)
    {
        _immune = false;
        _life = START_LIFE;
        _score = 0;
        _movement = movement;
        _weapon = new Weapon();
        _weapon.Equip(new PistolWeapon());
    }

    public void UpdateGame(float deltaTime)
    {
        _movement.UpdateRotation(deltaTime);
        _weapon.Update(deltaTime);

        if (_immune == true)
        {
            _immuneTimer -= deltaTime;
            if (_immuneTimer <= 0)
            {
                _immuneTimer = 0;
                _immune = false;
            }
        }
    }

    public void FixedUpdateGame(float fixedDeltaTime)
    {
        _movement.UpdatePosition(fixedDeltaTime);
    }

    public void Damage(int value)
    {
        if (value > 0 & _immune == false) 
        {
            _life -= value;
            if (_life <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        _life = 0;
        OnDeath?.Invoke();
    }

    public void ImmuneEffect(float duration)
    {
        _immune = true;
        _immuneTimer = duration;
    }

    public void AddScore(int value)
    {
        if (value > 0)
        {
            _score += value;
            OnAddScore?.Invoke(_score);
        }
    }
}
