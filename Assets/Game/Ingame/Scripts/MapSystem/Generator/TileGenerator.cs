using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileGenerator
{
    private Dictionary<Tile, bool> _isGenTile;

    public void Init(Map map)
    {
        _isGenTile = new Dictionary<Tile, bool>();

        foreach (Zone zone in map.zoneList)
        {
            foreach (Biome biome in zone.biomeList)
            {
                foreach (Tile tile in biome.tileList)
                    _isGenTile.Add(tile, false);
            }
        }
    }

    public void SetStartTile(Biome biome, TileManager.TileKind kind, int count)
    {
        List<Tile> incompleteTileList = GetIncompleteTile(biome);

        List<int> candTileIdx = Enumerable.Range(0, incompleteTileList.Count).ToList();
        List<int> startTileIdx = CustomRandom.GetUniqueIntRandom(count, candTileIdx);
        List<int> needRemove = new List<int>();

        Tile tempTile;
        while (startTileIdx.Count > 0)
        {
            needRemove.Clear();

            for (int i = 0; i < startTileIdx.Count; ++i)
            {
                tempTile = incompleteTileList[startTileIdx[i]];
                if (!_isGenTile[tempTile])
                {
                    tempTile.SetTile(kind);
                    _isGenTile[tempTile] = true;
                    biome.tileListAsKind[kind].Add(tempTile);

                    needRemove.Add(startTileIdx[i]);
                }
                candTileIdx.Remove(startTileIdx[i]);
            }

            foreach (int i in needRemove)
                startTileIdx.Remove(i);

            CustomRandom.GetUniqueIntRandom(startTileIdx, candTileIdx);
        }
    }

    private void MakeTileBranch(Tile tile, bool isCanOverlap)
    {

    }

    public List<Tile> GetIncompleteTile(Biome biome)
    {
        List<Tile> incompleteTileList = new List<Tile>();

        foreach (Tile tile in biome.tileList)
        {
            if (!_isGenTile[tile])
                incompleteTileList.Add(tile);
        }

        return incompleteTileList;
    }

    public int CheckGenComplete(Biome biome)
    {
        int notGenTileNum = 0;

        foreach (Tile tile in biome.tileList)
        {
            if (!_isGenTile[tile])
                ++notGenTileNum;
        }

        return notGenTileNum;
    }
}
