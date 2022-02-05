using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int mapSeed; // �׽�Ʈ�� ���� �ӽ� ����

    public Map map;
    public MapGenerator generator;

    private void Start()
    {
        GenerateMap(mapSeed);
    }

    public void GenerateMap(int seed)
    {
        generator.GenerateMap(seed);
    }

}
