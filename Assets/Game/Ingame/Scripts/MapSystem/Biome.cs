using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    public Zone zone;
    public List<Tile> tileList;
    public Biome[] neighbor = new Biome[4];    // 0: N // 1: W // 2: S // 3: E
    private int _size;

    public void MakeBiome(TileManager.TileInfo info)
    {
        foreach (Tile tile in tileList)
        {
            tile.ChangeKind(info);
        }
    }
}
