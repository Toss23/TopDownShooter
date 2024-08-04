using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Context : MonoBehaviour, IContext
{
    public event Action<bool> OnPause;
    public event Action<float> OnUpdateGame;
    public event Action<float> OnFixedUpdateGame;

    [SerializeField] private GameObject _loadingScreen;

    private AssetLoader _assetLoader;
    private List<IInitable> _initables;
    private List<IUpdatable> _updatable;

    private IDeathMenuPresenter _deathMenuPresenter;
    private Player _player;

    private bool _freezeGame;

    private async void Awake()
    {
        _loadingScreen.SetActive(true);
        StopGame();
        await LoadAssets();
        InitObjects();
        StartGame();
        _loadingScreen.SetActive(false);
    }

    private async Task LoadAssets()
    {
        _assetLoader = AssetLoader.Create();

        Type assetsType = typeof(Assets);
        foreach (FieldInfo field in assetsType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.FieldType == typeof(string))
            {
                string assetName = field.GetValue(assetsType).ToString();
                await _assetLoader.Load(assetName);
            }
        }
    }

    private void InitObjects()
    {
        _initables = new List<IInitable>();
        _updatable = new List<IUpdatable>();

        _initables.AddRange(FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IInitable>().ToArray());
        _updatable.AddRange(FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IUpdatable>().ToArray());

        foreach (IInitable initable in _initables)
        {
            initable.PreInit(this);
        }

        foreach (IInitable initable in _initables)
        {
            initable.Init(this);
        }

        foreach (IUpdatable updatable in _updatable)
        {
            AddUpdatable(updatable);
        }

        PostInit();
    }

    private void PostInit()
    {
        _deathMenuPresenter = GetObjectWithInterface<IDeathMenuPresenter>(typeof(DeathMenuPresenter));
        _player = GetObjectWithInterface<IPlayerPresenter>(typeof(PlayerPresenter)).Player;
    }

    private void Update()
    {
        if (_freezeGame == false)
        {
            OnUpdateGame?.Invoke(Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (_freezeGame == false)
        {
            OnFixedUpdateGame?.Invoke(Time.fixedDeltaTime);
        }
    }

    public void OnPlayerDeath()
    {
        StopGame();
        _deathMenuPresenter.DeathMenu.Show(_player.Score);
    }

    private void StartGame()
    {
        _freezeGame = false;
        OnPause?.Invoke(false);
    }

    private void StopGame()
    {
        _freezeGame = true;
        OnPause?.Invoke(true);
    }

    public void Initialize(IInitable initable)
    {
        initable.PreInit(this);
        initable.Init(this);
    }

    public void AddUpdatable(IUpdatable updatable)
    {
        OnUpdateGame += updatable.UpdateGame;
        OnFixedUpdateGame += updatable.FixedUpdateGame;
    }

    public void RemoveUpdatable(IUpdatable updatable)
    {
        OnUpdateGame -= updatable.UpdateGame;
        OnFixedUpdateGame -= updatable.FixedUpdateGame;
    }

    public T GetObject<T>()
    {
        if (typeof(T).IsInterface == true)
        {
            return default(T);
        }

        foreach (IInitable initable in _initables)
        {
            if (initable.GetType() == typeof(T))
            {
                return (T)initable;
            }
        }

        return default(T);
    }

    public T GetObjectWithInterface<T>(Type type)
    {
        foreach (IInitable initable in _initables)
        {
            if (initable.GetType() == type)
            {
                return (T)initable;
            }
        }

        return default(T);
    }

    public void EscapeFromGame()
    {
        StopGame();
        SceneManager.LoadScene("Menu");
    }
}