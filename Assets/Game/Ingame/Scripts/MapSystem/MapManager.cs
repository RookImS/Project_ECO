using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    public List<Zone> zoneList;

    private void Awake()
    {
        zoneList = new List<Zone>();
    }

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            zoneList.Add(transform.GetChild(i).GetComponent<Zone>());
        }
        
        MakeMap();
    }

    public void MakeMap()
    {
        TileManager.TileInfo tileInfo;
        for (int i = 0; i < zoneList.Count; i++)
        {
            tileInfo = TileManager.tileInfoList[i % TileManager.tileInfoList.Count];
            zoneList[i].MakeZone(tileInfo);
        }
    }
}
