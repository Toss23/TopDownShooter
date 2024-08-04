using UnityEngine;

public class ZonePresenter : MonoBehaviour, IZonePresenter
{
    private Zone _zone;

    public Zone Zone => _zone;

    public void SetZone(Zone zone)
    {
        _zone = zone;
    }
}
