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
        // �ּ�, �ִ� ������ ���� ���� ������ ����
        int startTileNum = Random.Range(min, max);

        // �� zone�� ���� Ÿ�� ������ ����
        List<int> zoneStartTileNum = CustomRandom.RandomlyDistributeNumber(startTileNum, zoneCount, Zone.biomeCount * Biome.tileCount);
            
        for (int i = 0; i < zoneCount; ++i)
            zoneList[i].SetStartTile(kind, zoneStartTileNum[i]);
    }


}
