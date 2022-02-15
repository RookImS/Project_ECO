using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Zone> zoneList;

    public static int size { get; private set; }
    public static int zoneCount { get; private set; }

    private void Awake()
    {
        size = (int)Mathf.Sqrt(zoneList.Count);
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

    public Tile FindTile(int tileRow, int tileCol)
    {
        int childRow = tileRow / (Zone.size * Biome.size);
        int childCol = tileCol / (Zone.size * Biome.size);

        return zoneList[childRow * size + childCol].FindTile(tileRow, tileCol);
    }
}
