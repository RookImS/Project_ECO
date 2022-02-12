using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    public enum kind
    {
        test1, test2
    };

    public Zone zone;
    public List<Tile> tileList;

    public int row;
    public int col;
    public Biome[] neighbor = new Biome[4];    // 0: N // 1: E // 2: S // 3: W

    [HideInInspector]
    public bool isEdge;
    public Dictionary<TileManager.TileKind, List<Tile>> tileListAsKind;

    public static int biomeSize { get; private set; }
    public static int tileCount { get; private set; }

    private void Awake()
    {
        biomeSize = (int)Mathf.Sqrt(tileList.Count);
        tileCount = tileList.Count;
    }

    public void Init()
    {
        foreach (Tile tile in tileList)
            tile.Init();

        tileListAsKind = new Dictionary<TileManager.TileKind, List<Tile>>();
        foreach (TileManager.TileKind kind in TileManager.GetEnumList<TileManager.TileKind>())
            tileListAsKind.Add(kind, new List<Tile>());
    }

    public List<Tile> GetEdgeTiles()
    {
        List<Tile> edgeTileList = new List<Tile>();

        foreach (Tile tile in tileList)
        {
            if (tile.isEdge)
                edgeTileList.Add(tile);
        }

        return edgeTileList;
    }

    public int GetRowDistance(Biome other)
    {
        return Mathf.Abs(row - other.row);
    }

    public int GetColDistance(Biome other)
    {
        return Mathf.Abs(col - other.col);
    }
}
