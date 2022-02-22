using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator
{
    private ZoneGenerator _zoneGenerator;

    public void Init(int seed, Map map)
    {
        Random.InitState(seed);

        _zoneGenerator = new ZoneGenerator();
        _zoneGenerator.Init(map);
    }

    public void SetStartTile(Map map, MapSetting mapSetting, TileManager.TileKind kind)
    {
        MapSetting.TileSetting? tileSetting = mapSetting.GetTileSetting(kind);

        if (tileSetting != null)
        {
            if (!_zoneGenerator.CheckGenComplete(map))
                _zoneGenerator.SetStartTile(map, tileSetting.Value);
        }
    }

    public void StretchTile(Map map, MapSetting mapSetting, TileManager.TileKind kind, bool canOverlap)
    {
        MapSetting.TileSetting? tileSetting = mapSetting.GetTileSetting(kind);

        if (tileSetting != null)
        {
            _zoneGenerator.StretchTile(map, tileSetting.Value, canOverlap);
        }
    }

    public void MakeRiver(Map map, MapSetting.RiverSetting riverSetting)
    {
        List<Tile> edgeTileList = map.GetEdgeTiles();

        List<Tile> riverMaker;
        for (int i = 0; i < riverSetting.num; ++i)
        {
            riverMaker = CustomRandom.GetElements(2, edgeTileList);

            edgeTileList.Remove(riverMaker[0]);

            while(riverMaker[0].GetColRowDistance(riverMaker[1]) <= 1.5 * Zone.size * Biome.size)
                riverMaker[1] = CustomRandom.GetElement(edgeTileList);

            edgeTileList.Remove(riverMaker[1]);

            _zoneGenerator.MakeRiver(map, riverSetting, riverMaker);
        }
    }
}
