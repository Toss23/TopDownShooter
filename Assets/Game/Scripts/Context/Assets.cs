using System;
using System.Collections.Generic;

public static class Assets
{
    public static readonly string BULLET_PREFAB = "BulletPrefab";
    public static readonly string EXPLODE_PREFAB = "ExplodePrefab";
    public static readonly string ENEMY_PREFAB = "EnemyPrefab";
    public static readonly string BONUS_PREFAB = "BonusPrefab";
    public static readonly string WEAPON_BONUS_PREFAB = "WeaponBonusPrefab";
    public static readonly string CHILL_ZONE_PREFAB_NAME = "ChillZonePrefab";
    public static readonly string DEATH_ZONE_PREFAB_NAME = "DeathZonePrefab";

    public static readonly Dictionary<Type, string> ZoneNames = new Dictionary<Type, string>()
    {
        [typeof(ChillZone)] = CHILL_ZONE_PREFAB_NAME,
        [typeof(DeathZone)] = DEATH_ZONE_PREFAB_NAME
    };
}
