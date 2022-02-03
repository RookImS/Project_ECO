using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public List<Biome> biomeList;
    public Zone[] neighbor = new Zone[4];  // 0: N // 1: W // 2: S // 3: E
    private int _size;

    public void MakeZone(TileManager.TileInfo info)
    {
        foreach (Biome biome in biomeList)
        {
            biome.MakeBiome(info);
        }
    }
}
