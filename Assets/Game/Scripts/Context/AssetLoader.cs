using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader
{
    private static AssetLoader Instance;

    private Dictionary<string, AsyncOperationHandle> _assets;

    private AssetLoader()
    {
        _assets = new Dictionary<string, AsyncOperationHandle>();
        Instance = this;
    }

    public static AssetLoader Create()
    {
        if (Instance != null)
        {
            Instance.ReleaseAll();
            Instance = null;
        }

        Instance = new AssetLoader();

        return Instance;
    }

    public static AssetLoader GetInstance()
    {
        if (Instance == null)
        {
            Debug.LogWarning("[AssetLoader] Instance was recreated. This can lead to memory leaks!");
            Create();
        }

        return Instance;
    }

    /// <summary>
    /// Load GameObject from addressable
    /// </summary>
    public async Task Load(string name)
    {
        await Load<GameObject>(name);
    }

    public async Task Load<T>(string name)
    {
        AsyncOperationHandle handle;

        if (_assets.ContainsKey(name))
        {
            handle = _assets[name];
            await handle.Task;
        }
        else
        {
            handle = Addressables.LoadAssetAsync<T>(name);
            await handle.Task;
            if (handle.Task.Status == TaskStatus.Faulted)
            {
                Debug.LogError("[AssetsLoader] Asset with name (" + name + ") not found");
            }
            else if (handle.Task.Status == TaskStatus.RanToCompletion)
            {
                _assets.Add(name, handle);
            }
        }
    }

    private AsyncOperationHandle GetHandle(string name)
    {
        if (_assets.ContainsKey(name))
        {
            return _assets[name];
        }
        else
        {
            Debug.LogError("[AssetsLoader] Assets list not contain (" + name + ") item");
            return new AsyncOperationHandle();
        }
    }

    public T GetAsset<T>(string name)
    {
        return (T)GetHandle(name).Result;
    }

    public void Release(string name)
    {
        if (_assets.ContainsKey(name))
        {
            AsyncOperationHandle handle = _assets[name];
            _assets.Remove(name);
            Addressables.Release(handle);
        }
    }

    public void ReleaseAll()
    {
        Dictionary<string, AsyncOperationHandle> assets = new Dictionary<string, AsyncOperationHandle>();
        foreach (KeyValuePair<string, AsyncOperationHandle> asset in _assets)
        {
            assets.Add(asset.Key, asset.Value);
        }

        foreach (KeyValuePair<string, AsyncOperationHandle> asset in assets)
        {
            Release(asset.Key);
        }
    }
}
