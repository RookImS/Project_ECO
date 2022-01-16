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

    void Update()
    {
        // 초기화
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);

        List<GameObject> zones = new List<GameObject>();
        // zone을 생성
        int zoneLength = tileScale * biomeSize * zoneSize;
        for (int i = 0; i < mapSize; ++i)
        {
            for (int j = 0; j < mapSize; ++j)
            {
                GameObject zoneObject = new GameObject("Zone" + (i * mapSize + j));
                zoneObject.transform.SetParent(transform);

                zoneObject.transform.localPosition = new Vector3(zoneLength * j, zoneLength * i, 0);
                zoneObject.AddComponent<Zone>();

                zones.Add(zoneObject);
            }
        }

        // zone 안에 biome 생성
        int biomeLength = tileScale * biomeSize;
        foreach (GameObject zone in zones)
        {
            for (int i = 0; i < zoneSize; ++i)
            {
                for (int j = 0; j < zoneSize; ++j)
                {
                    GameObject biomeObject = new GameObject("Biome" + (i * zoneSize + j));
                    biomeObject.transform.SetParent(zone.transform);

                    biomeObject.transform.localPosition = new Vector3(biomeLength * j, biomeLength * i, 0);
                    biomeObject.AddComponent<Biome>();

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
                        }
                    }
                }
            }
        }

        this.enabled = false;
    }
}
