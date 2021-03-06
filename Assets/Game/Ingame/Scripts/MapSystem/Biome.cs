using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    public enum kind
    {
        test1, test2
    };

    public int id;
    public Zone zone;
    public List<Tile> tileList;
    public static int tileCount { get; private set; }
    public Biome[] neighbor = new Biome[4];    // 0: N // 1: E // 2: S // 3: W

    public Dictionary<TileManager.TileKind, List<Tile>> tileListAsKind;

    private void Awake()
    {
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
}
