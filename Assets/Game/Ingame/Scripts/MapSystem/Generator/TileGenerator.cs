using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        List<int> startTileIdx = CustomRandom.GetUniqueInt(count, candTileIdx);
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

            CustomRandom.GetUniqueInt(startTileIdx, candTileIdx);
        }
    }

    public void StretchTile(Biome biome, MapSetting.TileSetting tileSetting, bool isCanOverlap)
    {
        List<Tile> startTileList = new List<Tile>(biome.tileListAsKind[tileSetting.kind]);
        foreach (Tile tile in startTileList)
        {
            _changedTile.Clear();
            int dir = Random.Range(0, 4);
            MakeTileBranch(tile, tileSetting.kind, tileSetting.stretchProba, 0, dir, isCanOverlap);
        }

        startTileList = new List<Tile>(biome.tileListAsKind[tileSetting.kind]);
        foreach (Tile tile in startTileList)
        {
            _changedTile.Clear();
            MakeTileSprawl(tile, tileSetting.kind, tileSetting.sprawlProba, isCanOverlap);
        }
    }

    private void MakeTileBranch(Tile tile, TileManager.TileKind kind, int stretchProba, int bendProba, int dir, bool isCanOverlap)
    {
        _changedTile.Add(tile);

        Tile neighborTile = tile.neighbor[dir];
        if (!ReferenceEquals(neighborTile, null))
        {
            if (_changedTile.Find(x => ReferenceEquals(x, neighborTile)) == null)
            {
                if (_isGenTile[neighborTile])
                {
                    if (isCanOverlap && CustomRandom.PickByProba(50))
                        goto BranchTile;
                }
                else
                    goto BranchTile;

            BranchTile:
                {
                    bool isChange;

                    isChange = CustomRandom.PickByProba(stretchProba);

                    if (isChange)
                    {
                        // 방향성을 바꾸게 될 것인가?
                        if (CustomRandom.PickByProba(bendProba))
                        {
                            bendProba = 0;
                            if (CustomRandom.PickByProba(50))
                                dir = (dir + 1) % 4;
                            else
                                dir = (dir + 3) % 4;
                        }
                        neighborTile.SetTile(kind);
                        _isGenTile[neighborTile] = true;
                        neighborTile.biome.tileListAsKind[kind].Add(neighborTile);
                        MakeTileBranch(neighborTile, kind, stretchProba - 7, bendProba + 3 , dir, isCanOverlap);
                    }
                }
            }
        }
    }

    private void MakeTileSprawl(Tile tile, TileManager.TileKind kind, int sprawlProba, bool isCanOverlap)
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
                        goto MakeTile;
                    }
                    else
                    {
                        if (!_isGenTile[neighborTile])
                            goto MakeTile;
                    }

                MakeTile:
                    {
                        bool isChange;

                        isChange = CustomRandom.PickByProba(sprawlProba);

                        if (isChange)
                        {
                            neighborTile.SetTile(kind);
                            _isGenTile[neighborTile] = true;
                            neighborTile.biome.tileListAsKind[kind].Add(neighborTile);
                            MakeTileSprawl(neighborTile, kind, sprawlProba - 5, isCanOverlap);
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
