using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    public List<GameObject> zoneList;

    private void Awake()
    {
        zoneList = new List<GameObject>();
    }
    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            zoneList.Add(transform.GetChild(i).gameObject);
        }
        
        MakeMap();
    }

    public void MakeMap()
    {
        Zone zone;
        TileManager.TileInfo tileInfo;
        for (int i = 0; i < zoneList.Count; i++)
        {
            tileInfo = TileManager.tileInfoList[i % TileManager.tileInfoList.Count];
            zone = zoneList[i].GetComponent<Zone>();
            zone.MakeZone(tileInfo);
        }
    }
}
