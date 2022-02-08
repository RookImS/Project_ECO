using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int test_mapSeed; // �׽�Ʈ�� ���� �ӽ� ����

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

        // �� ����
        mapGenerator.SetStartTile(TileManager.TileKind.Mountain);

        // �� ����
        mapGenerator.SetStartTile(TileManager.TileKind.Water);
    }

}
