using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;
    public Map map;
    public List<Biome> biomeList;
    public static int biomeCount { get; private set; }
    public Zone[] neighbor = new Zone[4];  // 0: N // 1: E // 2: S // 3: W

    private void Awake()
    {
        biomeCount = biomeList.Count;    
    }

    public void Init()
    {
        foreach (Biome biome in biomeList)
            biome.Init();
    }
}
