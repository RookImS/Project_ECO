using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct MapSetting
    {
        [System.Serializable]
        public struct StartTileMinMax
        {
            public TileManager.TileKind kind;
            public int minStart;
            public int maxStart;
        }

        public List<StartTileMinMax> startTileMinMaxSettings;
    }

    public Map map;
    public MapSetting mapSettings;

    private ZoneGenerator zoneGenerator;

    public void Init(int seed)
    {
        Random.InitState(seed);
        
        zoneGenerator = new ZoneGenerator();
        zoneGenerator.Init(map);
    }

    public void SetStartTile(TileManager.TileKind kind)
    {
        bool isFind = false;

        foreach (MapSetting.StartTileMinMax startTileMinMax in mapSettings.startTileMinMaxSettings)
        {
            if (startTileMinMax.kind == kind)
            {
                if (!zoneGenerator.CheckGenComplete(map.zoneList))
                {
                    List<Zone> incompleteZoneList = zoneGenerator.GetIncompleteZone(map.zoneList);
                    zoneGenerator.SetStartTile(incompleteZoneList, kind, startTileMinMax.minStart, startTileMinMax.maxStart);
                }

                isFind = true;
                break;
            }
        }

        if (!isFind)
            Debug.LogError(kind + "종류의 시작타일 설정을 찾을 수 없습니다. " + gameObject.name + "의 설정을 확인하세요.");
    }
}
