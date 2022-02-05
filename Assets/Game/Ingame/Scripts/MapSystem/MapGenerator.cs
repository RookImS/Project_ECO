using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct MapSetting
    {
        [Header("�� ������ ���� ����")]
        public int minStartMountain;
        public int maxStartMountain;
        [Header("�� ������ ���� ����")]
        public int minStartRiver;
        public int maxStartRiver;
    }

    public MapSetting mapSetting;
    public Map map;

    public void GenerateMap(int seed)
    {
        InitSeed(seed);

        map.Init();

        // �� ����
        map.SetStartTile(TileManager.TileKind.Mountain, mapSetting.minStartMountain, mapSetting.maxStartMountain);

        // �� ����
        map.SetStartTile(TileManager.TileKind.Water, mapSetting.minStartRiver, mapSetting.maxStartRiver);
    }

    private void InitSeed(int seed)
    {
        Random.InitState(seed);
    }


}
