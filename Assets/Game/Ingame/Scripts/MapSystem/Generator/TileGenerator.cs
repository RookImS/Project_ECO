using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileGenerator
{
    private Dictionary<Tile, bool> _isGenTile;
    private List<Tile> _changedTile;

    public void Init(Map map)
    {
        _isGenTile = new Dictionary<Tile, bool>();
        _changedTile = new List<Tile>();

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

    public void StretchTile(Biome biome, TileManager.TileKind kind, int proba, bool isCanOverlap)
    {
        foreach (Tile tile in biome.tileListAsKind[kind])
        {
            _changedTile.Clear();
            MakeTileBranch(tile, proba, isCanOverlap);
        }
    }

    private void MakeTileBranch(Tile tile, int proba, bool isCanOverlap)
    {
        _changedTile.Add(tile);

        foreach (Tile neighborTile in tile.neighbor)
        {
            if (!ReferenceEquals(neighborTile, null))       // 이웃 타일이 null이 아니면
            {
                if (_changedTile.Find(x => ReferenceEquals(x, neighborTile)) == null)   // 변경된 타일이 아니면
                {
                    if (isCanOverlap)
                    {
                        bool isChange = CustomRandom.RandomlyPickByProba(proba);

                        if (isChange)
                        {
                            neighborTile.SetTile(tile.kind);
                            _isGenTile[neighborTile] = true;
                            MakeTileBranch(neighborTile, proba, isCanOverlap);
                        }
                    }
                    else
                    {
                        if (!_isGenTile[neighborTile])
                        {
                            bool isChange = CustomRandom.RandomlyPickByProba(proba);

                            if (isChange)
                            {
                                neighborTile.SetTile(tile.kind);
                                _isGenTile[neighborTile] = true;
                                MakeTileBranch(neighborTile, proba, isCanOverlap);
                            }
                        }
                    }
                }
            }
        }
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
