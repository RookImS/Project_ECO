using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileArranger : MonoBehaviour
{
    [Tooltip("tile의 크기를 결정")]
    public int tileScale;
    [Tooltip("biome내 tile의 개수를 결정(가로상의 개수)")]
    public int biomeSize;
    [Tooltip("zone내 biome의 개수를 결정(가로상의 개수)")]
    public int zoneSize;
    [Tooltip("map내 zone의 개수를 결정(가로상의 개수)")]
    public int mapSize;

    public GameObject tilePrefab;

    private List<Zone> _zones;

    private int _zoneLength = 0;
    private int _biomeLength = 0;

    void Update()
    {
        // 초기화
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);

        _zoneLength = 0;
        _biomeLength = 0;

        MakeTile();

        SetNeighbor();


        this.enabled = false;
    }

    private void MakeTile()
    {
        _zones = new List<Zone>();
        // zone을 생성
        _zoneLength = tileScale * biomeSize * zoneSize;
        for (int i = 0; i < mapSize; ++i)
        {
            for (int j = 0; j < mapSize; ++j)
            {
                GameObject zoneObject = new GameObject("Zone" + (i * mapSize + j));
                zoneObject.transform.SetParent(transform);

                zoneObject.transform.localPosition = new Vector3(_zoneLength * j, _zoneLength * i, 0);
                zoneObject.AddComponent<Zone>();

                _zones.Add(zoneObject.GetComponent<Zone>());
            }
        }

        // zone 안에 biome 생성
        _biomeLength = tileScale * biomeSize;
        foreach (Zone zone in _zones)
        {
            zone.biomeList = new List<Biome>();

            for (int i = 0; i < zoneSize; ++i)
            {
                for (int j = 0; j < zoneSize; ++j)
                {
                    GameObject biomeObject = new GameObject("Biome" + (i * zoneSize + j));
                    biomeObject.transform.SetParent(zone.transform);

                    biomeObject.transform.localPosition = new Vector3(_biomeLength * j, _biomeLength * i, 0);
                    biomeObject.AddComponent<Biome>();

                    zone.biomeList.Add(biomeObject.GetComponent<Biome>());
                    zone.biomeList[i * zoneSize + j].tileList = new List<Tile>();

                    // biome 안에 tile 생성
                    for (int m = 0; m < biomeSize; ++m)
                    {
                        GameObject rowObject = new GameObject("Row" + m);
                        rowObject.transform.SetParent(biomeObject.transform);

                        rowObject.transform.localPosition = new Vector3(0, tileScale * m, 0);
                        for (int n = 0; n < biomeSize; ++n)
                        {
                            GameObject tileObject = Instantiate(tilePrefab, rowObject.transform);
                            tileObject.name = "Tile" + n;

                            tileObject.transform.localPosition = new Vector3(tileScale * n, 0, 0);
                            tileObject.transform.localScale = new Vector3(tileScale, tileScale, 0);

                            zone.biomeList[i * zoneSize + j].tileList.Add(tileObject.GetComponent<Tile>());
                        }
                    }
                }
            }
        }
    }

    private void SetNeighbor()
    {

    }

    private GameObject FindParentNeighbor(GameObject parentObject, int direction)      // 0: N // 1: W // 2: S // 3: E
    {
        GameObject parentNeighborObject = null;

        switch (direction)
        {
            case 0:         // North
                
                break;
            case 1:         // West

                break;
            case 2:         // South

                break;
            case 3:         // East

                break;
            default:
                Debug.Log("Failed to find neighbor");
                break;
        }
        


        return parentNeighborObject;
    }
}
