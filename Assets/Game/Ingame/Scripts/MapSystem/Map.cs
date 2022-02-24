using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Zone> zoneList;

    public static int size { get; private set; }
    public static int zoneCount { get; private set; }

    private Dictionary<TileManager.TileKind, int> _tileCountAsKind;

    private void Awake()
    {
        size = (int)Mathf.Sqrt(zoneList.Count);
        zoneCount = zoneList.Count;
    }

    public void Init()
    {
        _tileCountAsKind = new Dictionary<TileManager.TileKind, int>();
        foreach (TileManager.TileKind kind in TileManager.GetEnumList<TileManager.TileKind>())
            _tileCountAsKind.Add(kind, 0);

        foreach (Zone zone in zoneList)
            zone.Init();
    }

    public void SetTileCountAsKind(TileManager.TileKind prevKind, TileManager.TileKind currentKind)
    {
        if(prevKind != currentKind)
            --_tileCountAsKind[prevKind];
        ++_tileCountAsKind[currentKind];
    }

    public int GetTileCountAsKind(TileManager.TileKind kind)
    {
        return _tileCountAsKind[kind];
    }

    public List<Tile> GetEdgeTiles()
    {
        List<Tile> edgeTileList = new List<Tile>();

        foreach(Zone zone in zoneList)
        {
            if (zone.isEdge)
                edgeTileList.AddRange(zone.GetEdgeTiles());
        }

        return edgeTileList;
    }

    public Tile FindTile(int tileCol, int tileRow)
    {
        int childCol = tileCol / (Zone.size * Biome.size);
        int childRow = tileRow / (Zone.size * Biome.size);

        return zoneList[childRow * size + childCol].FindTile(tileCol, tileRow);
    }
}
