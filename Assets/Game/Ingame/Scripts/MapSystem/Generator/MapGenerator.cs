using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public class MapSetting
    {
        [System.Serializable]
        public struct TileSetting
        {
            [Tooltip("맵을 형성할 타일의 종류")]
            public TileManager.TileKind kind;
            [Tooltip("시작 타일이 있는 경우 시작 타일의 최소 개수")]
            public int minStart;
            [Tooltip("시작 타일이 있는 경우 시작 타일의 최대 개수")]
            public int maxStart;
            [Tooltip("시작 타일로부터 가지를 뻗는 형태로 맵을 만들 경우 주변으로 가지를 뻗을 확률 (%)")]
            [Range(0, 100)]
            public int stretchProba;
        }

        public List<TileSetting> tileSettings;

        public void Init()
        {
            // 값이 안맞는 것을 초기화
        }

        public TileSetting? GetTileSetting(TileManager.TileKind kind)
        {
            TileSetting result;

            foreach (TileSetting tileSetting in tileSettings)
            {
                if (tileSetting.kind == kind)
                {
                    result = tileSetting;
                    return result;
                }
            }

            Debug.LogError(kind + "에 대한 설정을 찾을 수 없습니다. " + ToString() + "의 설정을 확인하세요.");
            return null;
        }
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
        MapSetting.TileSetting? tileSetting = mapSettings.GetTileSetting(kind);

        if (tileSetting != null)
        {
            if (!zoneGenerator.CheckGenComplete(map))
                zoneGenerator.SetStartTile(map, kind, tileSetting.Value.minStart, tileSetting.Value.maxStart);
        }
    }

    public void StretchTile(TileManager.TileKind kind, bool isCanOverlap)
    {
        MapSetting.TileSetting? tileSetting = mapSettings.GetTileSetting(kind);

        if (tileSetting != null)
        {
            zoneGenerator.StretchTile(map, kind, tileSetting.Value.stretchProba, isCanOverlap);
        }
    }
}
