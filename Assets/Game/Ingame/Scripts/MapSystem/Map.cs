using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Zone> zoneList;

    public static int zoneCount { get; private set; }

    private void Awake()
    {
        zoneCount = zoneList.Count;
    }

    public void Init()
    {
        foreach (Zone zone in zoneList)
            zone.Init();
    }
}
