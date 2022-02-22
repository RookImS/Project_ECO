using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGenerator
{
    private BiomeGenerator _biomeGenerator;
    private Dictionary<Zone, int> _notGenTileNum;

    public void Init(Map map)
    {
        _biomeGenerator = new BiomeGenerator();
        _biomeGenerator.Init(map);

        _notGenTileNum = new Dictionary<Zone, int>();
        foreach (Zone zone in map.zoneList)
            _notGenTileNum.Add(zone, Zone.biomeCount * Biome.tileCount);
    }

    public void SetStartTile(Map map, MapSetting.TileSetting tileSetting)
    {
        // 최소, 최대 개수를 통해 실제 시작타일 개수를 구함
        int startTileNum = Random.Range(tileSetting.minStart, tileSetting.maxStart);

        // 아직 생성이 덜된 zone만을 다룸
        List<Zone> incompleteZoneList = GetIncompleteZone(map);

        // 각 zone의 시작 타일 개수를 정함 
        List<int> notGenTileNum = new List<int>();
        foreach (Zone zone in incompleteZoneList)
            notGenTileNum.Add(_notGenTileNum[zone]);
        List<int> zoneStartTileNum = CustomRandom.DistributeNumber(startTileNum, incompleteZoneList.Count, notGenTileNum);

        Zone tempZone;
        for (int i = 0; i < incompleteZoneList.Count; ++i)
        {
            tempZone = incompleteZoneList[i];
            _biomeGenerator.SetStartTile(tempZone, tileSetting.kind, zoneStartTileNum[i]);
        }
    }

    public void StretchTile(Map map, MapSetting.TileSetting tileSetting, bool canOverlap)
    {
        foreach (Zone zone in map.zoneList)
            _biomeGenerator.StretchTile(zone, tileSetting, canOverlap);
    }

    public void MakeRiver(Map map, MapSetting.RiverSetting riverSetting, List<Tile> riverMaker)
    {
        List<Zone> incompleteZoneList = GetIncompleteZone(map);
        List<Zone> candiateZoneList = new List<Zone>(incompleteZoneList);
        candiateZoneList.Remove(riverMaker[0].biome.zone);
        candiateZoneList.Remove(riverMaker[1].biome.zone);

        int riverPointCount = Random.Range(2, candiateZoneList.Count / 2);
        List<Zone> riverPointZoneList = CustomRandom.GetElements(riverPointCount, candiateZoneList);

        _biomeGenerator.MakeRiver(map, riverSetting, riverPointZoneList, riverMaker);
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
            _notGenTileNum[zone] = _biomeGenerator.CheckGenComplete(zone);
            notGenTileNum += _notGenTileNum[zone];
        }

        if (notGenTileNum > 0)
            isComplete = false;

        return isComplete;
    }
}
