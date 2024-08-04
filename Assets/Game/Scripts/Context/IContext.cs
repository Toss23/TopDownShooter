using System;

public interface IContext
{
    public event Action<bool> OnPause;
    public event Action<float> OnUpdateGame;
    public event Action<float> OnFixedUpdateGame;

    public void OnPlayerDeath();
    public void Initialize(IInitable initable);
    public void AddUpdatable(IUpdatable updatable);
    public void RemoveUpdatable(IUpdatable updatable);
    public T GetObject<T>();
    public T GetObjectWithInterface<T>(Type type);
    public void EscapeFromGame();
}
