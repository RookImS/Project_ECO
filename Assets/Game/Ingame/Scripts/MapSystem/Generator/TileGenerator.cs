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

    public void SetStartTile(List<Tile> tileList, TileManager.TileKind kind, int count)
    {
        List<int> candTileIdx = Enumerable.Range(0, tileList.Count).ToList();
        List<int> startTileIdx = CustomRandom.GetUniqueIntRandom(count, candTileIdx);
        List<int> needRemove = new List<int>();

        Tile tempTile;
        while (startTileIdx.Count > 0)
        {
            needRemove.Clear();

            for (int i = 0; i < startTileIdx.Count; ++i)
            {
                tempTile = tileList[startTileIdx[i]];
                if (!_isGenTile[tempTile])
                {
                    tempTile.SetTile(kind);
                    _isGenTile[tempTile] = true;
                    //tileIdxListAsKind[kind].Add(startTileIdx[i]);

                    needRemove.Add(startTileIdx[i]);
                }
                candTileIdx.Remove(startTileIdx[i]);
            }

            foreach (int i in needRemove)
                startTileIdx.Remove(i);

            CustomRandom.GetUniqueIntRandom(startTileIdx, candTileIdx);
        }
    }

    public List<Tile> GetIncompleteTile(List<Tile> tileList)
    {
        List<Tile> incompleteTileList = new List<Tile>();

        foreach (Tile tile in tileList)
        {
            if (!_isGenTile[tile])
                incompleteTileList.Add(tile);
        }

        return incompleteTileList;
    }

    public int CheckGenComplete(List<Tile> tileList)
    {
        int notGenTileNum = 0;

        foreach (Tile tile in tileList)
        {
            if (!_isGenTile[tile])
                ++notGenTileNum;
        }

        return notGenTileNum;
    }
}
