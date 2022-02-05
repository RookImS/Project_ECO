using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct MapSetting
    {
        [Header("산 시작점 개수 설정")]
        public int minStartMountain;
        public int maxStartMountain;
        [Header("강 시작점 개수 설정")]
        public int minStartRiver;
        public int maxStartRiver;
    }

    public MapSetting mapSetting;
    public Map map;

    public void GenerateMap(int seed)
    {
        InitSeed(seed);

        map.Init();

        // 산 설정
        map.SetStartTile(TileManager.TileKind.Mountain, mapSetting.minStartMountain, mapSetting.maxStartMountain);

        // 물 설정
        map.SetStartTile(TileManager.TileKind.Water, mapSetting.minStartRiver, mapSetting.maxStartRiver);
    }

    private void InitSeed(int seed)
    {
        Random.InitState(seed);
    }


}
