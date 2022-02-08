using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int test_mapSeed; // 테스트를 위한 임시 변수

    public Map map;
    public MapGenerator mapGenerator;

    private void Awake()
    {
        map.Init();
    }

    private void Start()
    {
        GenerateMap(test_mapSeed);
    }

    public void GenerateMap(int seed)
    {
        mapGenerator.Init(seed);

        // 산 설정
        mapGenerator.SetStartTile(TileManager.TileKind.Mountain);

        // 물 설정
        mapGenerator.SetStartTile(TileManager.TileKind.Water);
    }

}
