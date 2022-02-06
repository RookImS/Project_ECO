using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public static int tileCount;
    public Biome[] neighbor = new Biome[4];    // 0: N // 1: E // 2: S // 3: W

    public Dictionary<TileManager.TileKind, List<int>> tileIdxListAsKind;

    private int _size;

    private void Awake()
    {
        tileCount = tileList.Count;
    }

    public void Init()
    {
        foreach (Tile tile in tileList)
            tile.Init();

        tileIdxListAsKind = new Dictionary<TileManager.TileKind, List<int>>();
        foreach (TileManager.TileKind kind in TileManager.GetEnumList<TileManager.TileKind>())
            tileIdxListAsKind.Add(kind, new List<int>());
    }

    public void SetStartTile(TileManager.TileKind kind, int count)
    {
        List<int> candTileIdx = Enumerable.Range(0, tileCount).ToList();
        List<int> startTileIdx = CustomRandom.GetUniqueIntRandom(count, candTileIdx);
        List<int> needRemove = new List<int>();

        Tile tempTile;
        while(startTileIdx.Count > 0)
        {
            needRemove.Clear();

            for (int i = 0; i < startTileIdx.Count; ++i)
            {
                tempTile = tileList[startTileIdx[i]];
                if (!tempTile.CheckIsGenerated())
                {
                    tempTile.SetTile(kind);
                    tileIdxListAsKind[kind].Add(startTileIdx[i]);

                    needRemove.Add(startTileIdx[i]);
                }
                candTileIdx.Remove(startTileIdx[i]);
            }

            foreach (int i in needRemove)
                startTileIdx.Remove(i);

            CustomRandom.GetUniqueIntRandom(startTileIdx, candTileIdx);
        }
    }
}
