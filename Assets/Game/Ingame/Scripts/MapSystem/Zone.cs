using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public Map map;
    public List<Biome> biomeList;

    public int row;
    public int col;
    public Zone[] neighbor = new Zone[4];  // 0: N // 1: E // 2: S // 3: W

    [HideInInspector]
    public bool isEdge;

    public static int zoneSize { get; private set; }
    public static int biomeCount { get; private set; }

    private void Awake()
    {
        zoneSize = (int)Mathf.Sqrt(biomeList.Count);
        biomeCount = biomeList.Count;    
    }

    public void Init()
    {
        foreach (Biome biome in biomeList)
            biome.Init();
    }

    public List<Tile> GetEdgeTiles()
    {
        List<Tile> edgeTileList = new List<Tile>();

        foreach (Biome biome in biomeList)
        {
            if (biome.isEdge)
                edgeTileList.AddRange(biome.GetEdgeTiles());
        }

        return edgeTileList;
    }

    public int GetRowDistance(Zone other)
    {
        return Mathf.Abs(row - other.row);
    }

    public int GetColDistance(Zone other)
    {
        return Mathf.Abs(col - other.col);
    }

}
