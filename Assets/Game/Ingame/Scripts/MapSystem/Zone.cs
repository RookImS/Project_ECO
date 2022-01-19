using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public List<GameObject> biomeList;
    private int _size;

    private void Awake()
    {
        List<GameObject> biomeList = new List<GameObject>();
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            biomeList.Add(transform.GetChild(i).gameObject);
        }
    }

    public void MakeZone(TileManager.TileInfo info)
    {
        Biome biome;
        foreach (GameObject biomeObject in biomeList)
        {
            biome = biomeObject.GetComponent<Biome>();
            biome.MakeBiome(info);
        }
    }
}
