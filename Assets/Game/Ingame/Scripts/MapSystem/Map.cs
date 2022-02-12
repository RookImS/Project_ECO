using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Zone> zoneList;

    public static int mapSize { get; private set; }
    public static int zoneCount { get; private set; }

    private void Awake()
    {
        mapSize = (int)Mathf.Sqrt(zoneList.Count);
        zoneCount = zoneList.Count;
    }

    public void Init()
    {
        foreach (Zone zone in zoneList)
            zone.Init();
    }

    public List<Tile> GetEdgeTiles()
    {
        List<Tile> edgeTileList = new List<Tile>();

        foreach(Zone zone in zoneList)
        {
            if (zone.isEdge)
                edgeTileList.AddRange(zone.GetEdgeTiles());
        }

        return edgeTileList;
    }
}
