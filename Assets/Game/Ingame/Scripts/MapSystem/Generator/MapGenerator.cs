using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct MapSetting
    {
        [System.Serializable]
        public struct TileSetting
        {
            public TileManager.TileKind kind;
            public int minStart;
            public int maxStart;
        }

        public List<TileSetting> tileSettings;
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
        MapSetting.TileSetting? tileSetting = GetTileSetting(kind);

        if (tileSetting != null)
        {
            if (!zoneGenerator.CheckGenComplete(map))
                zoneGenerator.SetStartTile(map, kind, tileSetting.Value.minStart, tileSetting.Value.maxStart);
        }
    }

    private MapSetting.TileSetting? GetTileSetting(TileManager.TileKind kind)
    {
        MapSetting.TileSetting result;

        foreach (MapSetting.TileSetting tileSetting in mapSettings.tileSettings)
        {
            if (tileSetting.kind == kind)
            {
                result = tileSetting;
                return result;
            }
        }

        Debug.LogError(kind + "에 대한 설정을 찾을 수 없습니다. " + gameObject.name + "의 설정을 확인하세요.");
        return null;
    }
}
