using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    public List<GameObject> tileList;
    private int _size;

    private void Awake()
    {
        tileList = new List<GameObject>();
    }

    private void Start()
    {
        // tileList�� �ε��� = _size * i + j  -->  i��° row�� j��° tile
        for (int i = 0; i < transform.childCount; ++i)
        {
            GameObject row = transform.GetChild(i).gameObject;
            for (int j = 0; j < row.transform.childCount; ++j)
            {
                tileList.Add(row.transform.GetChild(j).gameObject);
            }
        }
    }

    public void MakeBiome(TileManager.TileInfo info)
    {
        Tile tile;
        foreach (GameObject tileObject in tileList)
        {
            tile = tileObject.GetComponent<Tile>();
            tile.ChangeKind(info);
        }
    }
}
