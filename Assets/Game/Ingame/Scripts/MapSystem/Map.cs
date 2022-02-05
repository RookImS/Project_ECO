using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Zone> zoneList;
    public static int zoneCount;

    private void Awake()
    {
        zoneCount = zoneList.Count;
    }

    public void Init()
    {
        foreach (Zone zone in zoneList)
            zone.Init();
    }

    public void SetStartTile(TileManager.TileKind kind, int min, int max)
    {
        // 최소, 최대 개수를 통해 실제 개수를 구함
        int startTileNum = Random.Range(min, max);

        // 각 zone의 시작 타일 개수를 정함
        List<int> zoneStartTileNum = CustomRandom.RandomlyDistributeNumber(startTileNum, zoneCount, Zone.biomeCount * Biome.tileCount);
            
        for (int i = 0; i < zoneCount; ++i)
            zoneList[i].SetStartTile(kind, zoneStartTileNum[i]);
    }


}
