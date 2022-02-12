using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int test_mapSeed; // 테스트를 위한 임시 변수

    public Map map;
    public MapGenerator mapGenerator;
    public MapSetting mapSetting;
    public List<Tile> test;

    private void Awake()
    {
        map.Init();
        mapGenerator = new MapGenerator();
    }

    private void Start()
    {
        GenerateMap(test_mapSeed);
    }

    public void GenerateMap(int seed)
    {
        mapGenerator.Init(seed, map, mapSetting);

        // 산 설정
        mapGenerator.SetStartTile(map, mapSetting, TileManager.TileKind.Mountain);
        mapGenerator.StretchTile(map, mapSetting, TileManager.TileKind.Mountain, false);

        // 물 설정
        mapGenerator.SetStartTile(map, mapSetting, TileManager.TileKind.Water);
        mapGenerator.StretchTile(map, mapSetting, TileManager.TileKind.Water, true);
    }
}
