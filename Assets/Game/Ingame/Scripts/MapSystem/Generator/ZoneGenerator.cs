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

    public void SetStartTile(Map map, TileManager.TileKind kind, int min, int max)
    {
        // 최소, 최대 개수를 통해 실제 시작타일 개수를 구함
        int startTileNum = Random.Range(min, max);

        // 아직 생성이 덜된 zone만을 다룸
        List<Zone> incompleteZoneList = GetIncompleteZone(map);

        // 각 zone의 시작 타일 개수를 정함 
        List<int> notGenTileNum = new List<int>();
        foreach (Zone zone in incompleteZoneList)
            notGenTileNum.Add(_notGenTileNum[zone]);
        List<int> zoneStartTileNum = CustomRandom.RandomlyDistributeNumber(startTileNum, incompleteZoneList.Count, notGenTileNum);

        Zone tempZone;
        for (int i = 0; i < incompleteZoneList.Count; ++i)
        {
            tempZone = incompleteZoneList[i];            
            biomeGenerator.SetStartTile(tempZone, kind, zoneStartTileNum[i]);
        }
    }

    public List<Zone> GetIncompleteZone(Map map)
    {
        List<Zone> incompleteZoneList = new List<Zone>();

        foreach (Zone zone in map.zoneList)
        {
            if (_notGenTileNum[zone] > 0)
                incompleteZoneList.Add(zone);
        }

        return incompleteZoneList;
    }

    public bool CheckGenComplete(Map map)
    {
        bool isComplete = true;
        int notGenTileNum = 0;

        foreach (Zone zone in map.zoneList)
        {
            _notGenTileNum[zone] = biomeGenerator.CheckGenComplete(zone);
            notGenTileNum += _notGenTileNum[zone];
        }

        if (notGenTileNum > 0)
            isComplete = false;

        return isComplete;
    }
}
