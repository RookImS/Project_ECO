using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileArranger : MonoBehaviour
{
    private enum direction
    {
        N = 0, E = 1, S = 2, W = 3
    };

    public Map map;
    public GameObject tilePrefab;

    [Header("ũ�� ����")]
    [Tooltip("map�� zone�� ������ ����(���λ��� ����)")]
    public int mapSize;
    [Tooltip("zone�� biome�� ������ ����(���λ��� ����)")]
    public int zoneSize;
    [Tooltip("biome�� tile�� ������ ����(���λ��� ����)")]
    public int biomeSize;
    [Tooltip("tile�� ũ�⸦ ����")]
    public int tileScale;

    private int _zoneLength = 0;
    private int _biomeLength = 0;

    void Update()
    {
        // �ʱ�ȭ
        for (int i = map.transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(map.transform.GetChild(i).gameObject);

        _zoneLength = 0;
        _biomeLength = 0;

        MakeTile();

        SetNeighbor();

        this.enabled = false;
    }

    private void MakeTile()
    {
        map.zoneList = new List<Zone>();
        // zone�� ����
        Zone tempZone;
        _zoneLength = tileScale * biomeSize * zoneSize;
        for (int i = 0; i < mapSize; ++i)
        {
            for (int j = 0; j < mapSize; ++j)
            {
                GameObject zoneObject = new GameObject("Zone" + (i * mapSize + j));
                zoneObject.transform.SetParent(map.transform);

                zoneObject.transform.localPosition = new Vector3(_zoneLength * j, _zoneLength * i, 0);
                zoneObject.AddComponent<Zone>();
                tempZone = zoneObject.GetComponent<Zone>();
                tempZone.id = i * mapSize + j;

                map.zoneList.Add(tempZone);
            }
        }

        // zone �ȿ� biome ����
        _biomeLength = tileScale * biomeSize;
        Biome tempBiome;
        Tile tempTile;
        foreach (Zone zone in map.zoneList)
        {
            zone.biomeList = new List<Biome>();
            for (int i = 0; i < zoneSize; ++i)  // biomeRow
            {
                for (int j = 0; j < zoneSize; ++j)  // biomeCol
                {
                    GameObject biomeObject = new GameObject("Biome" + (i * zoneSize + j));
                    biomeObject.transform.SetParent(zone.transform);

                    biomeObject.transform.localPosition = new Vector3(_biomeLength * j, _biomeLength * i, 0);
                    biomeObject.AddComponent<Biome>();

                    tempBiome = biomeObject.GetComponent<Biome>();
                    tempBiome.zone = zone;
                    tempBiome.id = i * zoneSize + j;

                    zone.biomeList.Add(tempBiome);

                    // biome �ȿ� tile ����
                    zone.biomeList[i * zoneSize + j].tileList = new List<Tile>();
                    for (int m = 0; m < biomeSize; ++m) // tileRow
                    {
                        GameObject rowObject = new GameObject("Row" + m);
                        rowObject.transform.SetParent(biomeObject.transform);

                        rowObject.transform.localPosition = new Vector3(0, tileScale * m, 0);
                        for (int n = 0; n < biomeSize; ++n) //tileCol
                        {
                            GameObject tileObject = Instantiate(tilePrefab, rowObject.transform);
                            tileObject.name = "Tile" + n;

                            tileObject.transform.localPosition = new Vector3(tileScale * n, 0, 0);
                            tileObject.transform.localScale = new Vector3(tileScale, tileScale, 0);

                            tempTile = tileObject.GetComponent<Tile>();
                            tempTile.biome = tempBiome;
                            tempTile.id = m * biomeSize + n;

                            zone.biomeList[i * zoneSize + j].tileList.Add(tempTile);
                        }
                    }
                }
            }
        }
    }

    private void SetNeighbor()
    {
        int col = 0;
        int row = 0;
        int dir = 0;

        // ��ȹ ó��
        for (int i = 0; i < mapSize * mapSize; ++i)
        {
            col = i % mapSize;
            row = i / mapSize;

            // 0: N // 1: E // 2: S // 3: W
            // ���� ó��
            dir = (int)direction.N;
            if (row + 1 >= mapSize)
                map.zoneList[i].neighbor[dir] = null;
            else
                map.zoneList[i].neighbor[dir] = map.zoneList[i + mapSize];

            // ���� ó��
            dir = (int)direction.E;
            if (col + 1 >= mapSize)
                map.zoneList[i].neighbor[dir] = null;
            else
                map.zoneList[i].neighbor[dir] = map.zoneList[i + 1];

            // ���� ó��
            dir = (int)direction.S;
            if (row - 1 < 0)
                map.zoneList[i].neighbor[dir] = null;
            else
                map.zoneList[i].neighbor[dir] = map.zoneList[i - mapSize];

            // ���� ó��
            dir = (int)direction.W;
            if (col - 1 < 0)
                map.zoneList[i].neighbor[dir] = null;
            else
                map.zoneList[i].neighbor[dir] = map.zoneList[i - 1];
        }

        // ���̿� ó��
        foreach (Zone zone in map.zoneList)
        {
            for (int i = 0; i < zoneSize * zoneSize; ++i)
            {
                col = i % zoneSize;
                row = i / zoneSize;

                // ���� ó��
                dir = (int)direction.N;
                if (row + 1 >= zoneSize)
                {
                    if (ReferenceEquals(zone.neighbor[dir], null))
                        zone.biomeList[i].neighbor[dir] = null;
                    else
                        zone.biomeList[i].neighbor[dir] = zone.neighbor[dir].biomeList[col];
                }
                else
                    zone.biomeList[i].neighbor[dir] = zone.biomeList[i + zoneSize];

                // ���� ó��
                dir = (int)direction.E;
                if (col + 1 >= zoneSize)
                {
                    if (ReferenceEquals(zone.neighbor[dir], null))
                        zone.biomeList[i].neighbor[dir] = null;
                    else
                        zone.biomeList[i].neighbor[dir] = zone.neighbor[dir].biomeList[row * zoneSize];
                }
                else
                    zone.biomeList[i].neighbor[dir] = zone.biomeList[i + 1];

                // ���� ó��
                dir = (int)direction.S;
                if (row - 1 < 0)
                {
                    if (ReferenceEquals(zone.neighbor[dir], null))
                        zone.biomeList[i].neighbor[dir] = null;
                    else
                        zone.biomeList[i].neighbor[dir] = zone.neighbor[dir].biomeList[(zoneSize - 1) * zoneSize + col];
                }
                else
                    zone.biomeList[i].neighbor[dir] = zone.biomeList[i - zoneSize];

                // ���� ó��
                dir = (int)direction.W;
                if (col - 1 < 0)
                {
                    if (ReferenceEquals(zone.neighbor[dir], null))
                        zone.biomeList[i].neighbor[dir] = null;
                    else
                        zone.biomeList[i].neighbor[dir] = zone.neighbor[dir].biomeList[row * zoneSize + zoneSize - 1];
                }
                else
                    zone.biomeList[i].neighbor[dir] = zone.biomeList[i - 1];
            }
        }

        // Ÿ�� ó��
        foreach (Zone zone in map.zoneList)
        {
            foreach (Biome biome in zone.biomeList)
            {
                for (int i = 0; i < biomeSize * biomeSize; ++i)
                {
                    col = i % biomeSize;
                    row = i / biomeSize;

                    // ���� ó��
                    dir = (int)direction.N;
                    if (row + 1 >= biomeSize)
                    {
                        if (ReferenceEquals(biome.neighbor[dir], null))
                            biome.tileList[i].neighbor[dir] = null;
                        else
                            biome.tileList[i].neighbor[dir] = biome.neighbor[dir].tileList[col];
                    }
                    else
                        biome.tileList[i].neighbor[dir] = biome.tileList[i + biomeSize];

                    // ���� ó��
                    dir = (int)direction.E;
                    if (col + 1 >= biomeSize)
                    {
                        if (ReferenceEquals(biome.neighbor[dir], null))
                            biome.tileList[i].neighbor[dir] = null;
                        else
                            biome.tileList[i].neighbor[dir] = biome.neighbor[dir].tileList[row * biomeSize];
                    }
                    else
                        biome.tileList[i].neighbor[dir] = biome.tileList[i + 1];

                    // ���� ó��
                    dir = (int)direction.S;
                    if (row - 1 < 0)
                    {
                        if (ReferenceEquals(biome.neighbor[dir], null))
                            biome.tileList[i].neighbor[dir] = null;
                        else
                            biome.tileList[i].neighbor[dir] = biome.neighbor[dir].tileList[(biomeSize - 1) * biomeSize + col];
                    }
                    else
                        biome.tileList[i].neighbor[dir] = biome.tileList[i - biomeSize];

                    // ���� ó��
                    dir = (int)direction.W;
                    if (col - 1 < 0)
                    {
                        if (ReferenceEquals(biome.neighbor[dir], null))
                            biome.tileList[i].neighbor[dir] = null;
                        else
                            biome.tileList[i].neighbor[dir] = biome.neighbor[dir].tileList[row * biomeSize + biomeSize - 1];
                    }
                    else
                        biome.tileList[i].neighbor[dir] = biome.tileList[i - 1];
                }
            }
        }
    }
}
