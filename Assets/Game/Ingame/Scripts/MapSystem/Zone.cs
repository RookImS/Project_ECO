using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;
    public Map map;
    public List<Biome> biomeList;
    public static int biomeCount;
    public Zone[] neighbor = new Zone[4];  // 0: N // 1: W // 2: S // 3: E

    private int _size;

    private void Awake()
    {
        biomeCount = biomeList.Count;    
    }

    public void Init()
    {
        foreach (Biome biome in biomeList)
            biome.Init();
    }


    public void SetStartTile(TileManager.TileKind kind, int count)
    {
        // 각 biome의 시작 타일 개수를 정함
        List<int> biomeStartTileNum = CustomRandom.RandomlyDistributeNumber(count, biomeCount, Biome.tileCount);

        for (int i = 0; i < biomeCount; ++i)
            biomeList[i].SetStartTile(kind, biomeStartTileNum[i]);
    }
}
