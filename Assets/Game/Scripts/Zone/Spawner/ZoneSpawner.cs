using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawner
{
    private const int MAP_HEIGHT = 30;
    private const int MAP_WEDTH = 40;
    private const int MAP_SIDE_OFFSET = 3;

    private const int ZONE_MAX_RADIUS = 3;
    private const int CHILL_ZONE_COUNT = 3;
    private const int DEATH_ZONE_COUNT = 2;

    private List<Vector2> _spawnPoints;
    private List<Zone> _zones;

    public List<Zone> Zones => _zones;

    public ZoneSpawner()
    {
        // Генерируем точки центров для зон, удаляем лишнии, перемешиваем и пробуем сдвинуть
        _spawnPoints = GenerateSpawnPoints(ZONE_MAX_RADIUS);
        _spawnPoints = RemoveRandomPoints(_spawnPoints, _spawnPoints.Count - (CHILL_ZONE_COUNT + DEATH_ZONE_COUNT));
        _spawnPoints = ShufflePoints(_spawnPoints);
        _spawnPoints = TryRandomMovePoints(_spawnPoints, 1, 20);

        // Создаем зоны
        _zones = new List<Zone>();
        _zones.AddRange(TryGenerateZones<ChillZone>(CHILL_ZONE_COUNT));
        _zones.AddRange(TryGenerateZones<DeathZone>(DEATH_ZONE_COUNT));
    }

    /// <summary>
    /// Генерирует замощение шестиугольников (в которые можно вписать круг заданного радиуса) и берет их центры
    /// </summary>
    private List<Vector2> GenerateSpawnPoints(float radius)
    {
        List<Vector2> spawnPoints = new List<Vector2>();

        float deltaX = radius * 2 + MAP_SIDE_OFFSET;
        float deltaY = Mathf.Sqrt(0.75f * deltaX * deltaX);
        Vector2 deltaPosition = new Vector2(4.6f, 0);

        for (int x = -5; x < 10; x++)
        {
            for (int y = -5; y < 10; y++)
            {
                Vector2 position = deltaPosition + new Vector2(x * deltaX + (y % 2 == 0 ? deltaX / 2 : 0), y * deltaY);

                if (InPermittedArea(position) == false)
                {
                    continue;
                }

                spawnPoints.Add(position);
            }
        }

        return spawnPoints;
    }

    private List<Vector2> RemoveRandomPoints(List<Vector2> points, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, points.Count);
            points.RemoveAt(index);
        }

        return points;
    }

    private List<Vector2> ShufflePoints(List<Vector2> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, points.Count);
            Vector2 cache = points[randomIndex];
            points[randomIndex] = points[i];
            points[i] = cache;
        }

        return points;
    }

    private List<Vector2> TryRandomMovePoints(List<Vector2> points, float magnitude, int repeats)
    {
        for (int r = 0; r < repeats; r++)
        {
            for (int i = 0; i < points.Count; i++)
            {
                float deltaX = UnityEngine.Random.Range(-magnitude, magnitude);
                float deltaY = UnityEngine.Random.Range(-magnitude, magnitude);
                Vector2 deltaPosition = new Vector2(deltaX, deltaY);
                Vector2 newPosition = deltaPosition + new Vector2(points[i].x, points[i].y);

                if (InPermittedArea(newPosition) == false)
                {
                    continue;
                }

                bool collide = false;

                for (int j = 0; j < points.Count; j++)
                {
                    if (i != j & Vector2.Distance(newPosition, points[j]) <= 9)
                    {
                        collide = true;
                        break;
                    }
                }

                if (collide == false)
                {
                    points[i] = newPosition;
                }
            }
        }

        return points;
    }

    private bool InPermittedArea(Vector2 position)
    {
        if (position.x > -3 & position.x < 3 & position.y > -3 & position.y < 3)
        {
            return false;
        }

        if (position.x < -MAP_WEDTH / 2 + (MAP_SIDE_OFFSET + ZONE_MAX_RADIUS) |
            position.x > MAP_WEDTH / 2 - (MAP_SIDE_OFFSET + ZONE_MAX_RADIUS) |
            position.y < -MAP_HEIGHT / 2 + (MAP_SIDE_OFFSET + ZONE_MAX_RADIUS) |
            position.y > MAP_HEIGHT / 2 - (MAP_SIDE_OFFSET + ZONE_MAX_RADIUS))
        {
            return false;
        }

        return true;
    }

    private List<Zone> TryGenerateZones<T>(int count)
    {
        if (typeof(T).IsSubclassOf(typeof(Zone)))
        {
            List<Zone> zones = new List<Zone>();
            for (int i = 0; i < count; i++)
            {
                if (_spawnPoints.Count > 0)
                {
                    Vector2 point = _spawnPoints[_spawnPoints.Count - 1];
                    Zone zone = (Zone)Activator.CreateInstance(typeof(T), point.x, point.y);
                    zones.Add(zone);
                    _spawnPoints.RemoveAt(_spawnPoints.Count - 1);
                }
            }
            return zones;
        }

        return null;
    }
}
