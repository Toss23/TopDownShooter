using UnityEngine;

public class ZoneSpawnerPresenter : MonoBehaviour, IInitable
{
    private ZoneSpawner _zoneSpawner;
    private AssetLoader _assetLoader;

    public void PreInit(IContext context)
    {
        _zoneSpawner = new ZoneSpawner();
        _assetLoader = AssetLoader.GetInstance();
    }

    public void Init(IContext context)
    {
        foreach (Zone zone in _zoneSpawner.Zones)
        {
            CreateZone(zone);
        }
    }

    private IZonePresenter CreateZone(Zone zone)
    {
        GameObject prefab = _assetLoader.GetAsset<GameObject>(Assets.ZoneNames[zone.GetType()]);
        GameObject zoneGameObject = Instantiate(prefab, transform);
        zoneGameObject.name = zone.GetType().ToString();
        zoneGameObject.transform.position = new Vector2(zone.X, zone.Y);

        IZonePresenter zonePresenter = zoneGameObject.GetComponent<IZonePresenter>();
        zonePresenter.SetZone(zone);

        return zonePresenter;
    }
}
