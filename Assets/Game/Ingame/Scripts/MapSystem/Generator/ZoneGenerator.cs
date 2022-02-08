using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGenerator
{
    private BiomeGenerator biomeGenerator;
    private Dictionary<Zone, int> _notGenTileNum;
    
    public void Init(Map map)
    {
        biomeGenerator = new BiomeGenerator();
        biomeGenerator.Init(map);

        _notGenTileNum = new Dictionary<Zone, int>();
        foreach(Zone zone in map.zoneList)
            _notGenTileNum.Add(zone, Zone.biomeCount * Biome.tileCount);
    }

    public void SetStartTile(List<Zone> zoneList, TileManager.TileKind kind, int min, int max)
    {
        // 최소, 최대 개수를 통해 실제 개수를 구함
        int startTileNum = Random.Range(min, max);

        // 각 zone의 시작 타일 개수를 정함 
        List<int> notGenTileNum = new List<int>();
        foreach (Zone zone in zoneList)
            notGenTileNum.Add(_notGenTileNum[zone]);
        List<int> zoneStartTileNum = CustomRandom.RandomlyDistributeNumber(startTileNum, zoneList.Count, notGenTileNum);

        Zone tempZone;
        List<Biome> incompleteBiomeList;
        for (int i = 0; i < zoneList.Count; ++i)
        {
            tempZone = zoneList[i];
            incompleteBiomeList = biomeGenerator.GetIncompleteBiome(tempZone.biomeList);
            biomeGenerator.SetStartTile(incompleteBiomeList, kind, zoneStartTileNum[i]);
        }
    }

    public List<Zone> GetIncompleteZone(List<Zone> zoneList)
    {
        List<Zone> incompleteZoneList = new List<Zone>();

        foreach (Zone zone in zoneList)
        {
            if (_notGenTileNum[zone] > 0)
                incompleteZoneList.Add(zone);
        }

        return incompleteZoneList;
    }

    public bool CheckGenComplete(List<Zone> zoneList)
    {
        bool isComplete = true;
        int notGenTileNum = 0;

        foreach (Zone zone in zoneList)
        {
            _notGenTileNum[zone] = biomeGenerator.CheckGenComplete(zone.biomeList);
            notGenTileNum += _notGenTileNum[zone];
        }

        if (notGenTileNum > 0)
            isComplete = false;

        return isComplete;
    }
}
