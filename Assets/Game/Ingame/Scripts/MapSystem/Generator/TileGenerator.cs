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

    private void GenerateTile(Tile tile, TileManager.TileKind kind)
    {
        tile.SetTile(kind);
        _isGenTile[tile] = true;
    }

    public void SetStartTile(Biome biome, TileManager.TileKind kind, int count)
    {
        List<Tile> incompleteTileList = GetIncompleteTile(biome);
        List<Tile> startTileList = CustomRandom.GetElements(count, incompleteTileList);

        foreach (Tile tile in startTileList)
            GenerateTile(tile, kind);
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
                        GenerateTile(neighborTile, kind);
                        MakeTileBranch(neighborTile, kind, stretchProba - 7, bendProba + 3, dir, isCanOverlap);
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
                            GenerateTile(neighborTile, kind);
                            MakeTileSprawl(neighborTile, kind, sprawlProba - 5, isCanOverlap);
                        }
                    }
                }
            }
        }
    }

    public void MakeRiver(Map map, List<Biome> riverPointBiomeList, List<Tile> riverMaker)
    {
        List<Tile> incompleteTileList;

        foreach (Biome biome in riverPointBiomeList)
        {
            incompleteTileList = GetIncompleteTile(biome);
            riverMaker.Insert(1, CustomRandom.GetElement(incompleteTileList));
        }

        for (int i = 0; i < riverMaker.Count - 1; ++i)
        {
            Tile startTile = riverMaker[i];
            Tile endTile = riverMaker[i + 1];

            List<int> colRange, rowRange;
            List<Vector2> detailRiverPoint = GetDetailPoint(startTile, endTile);
            List<Tile> candiateTileList = new List<Tile>();
            Vector2 startPoint, endPoint;
            for (int k = 0; k < detailRiverPoint.Count - 1; ++k)
            {
                startPoint = detailRiverPoint[k];
                endPoint = detailRiverPoint[k + 1];

                List<float> lineEquation = CustomTool.MakeLineEquation(
                    startPoint, endPoint);

                colRange = CustomTool.MakeRange(Tile.XToCol(startPoint.x), Tile.XToCol(endPoint.x));
                rowRange = CustomTool.MakeRange(Tile.YToRow(startPoint.y), Tile.YToRow(endPoint.y));
                candiateTileList.Clear();
                candiateTileList = GetTileInRange(map, colRange, rowRange);
                ConnectRiver(candiateTileList, lineEquation);
            }
        }
    }

    private List<Tile> GetTileInRange(Map map, List<int> colRange, List<int> rowRange)
    {
        List<Tile> tileListInRange = new List<Tile>();
        Tile tempTile;

        int wholeTileNum = Map.size * Zone.size * Biome.size - 1;

        int colMin = colRange[0] < 0 ? 0 : colRange[0];
        int rowMin = rowRange[0] < 0 ? 0 : rowRange[0];
        int colMax = colRange[1] > wholeTileNum ? wholeTileNum : colRange[1];
        int rowMax = rowRange[1] > wholeTileNum ? wholeTileNum : rowRange[1];

        for (int j = rowMin; j <= rowMax; ++j)
        {
            for (int i = colMin; i <= colMax; ++i)
            {
                tempTile = map.FindTile(i, j);

                if (tempTile.kind != TileManager.TileKind.Water)
                    tileListInRange.Add(tempTile);
            }
        }

        return tileListInRange;
    }

    private List<Vector2> GetDetailPoint(Tile startTile, Tile endTile)
    {
        Vector2 riverDirVec = new Vector2(endTile.col - startTile.col, endTile.row - startTile.row);
        float riverDirAngle = Vector2.SignedAngle(new Vector2(1, 0), riverDirVec.normalized);
        Vector2 deltaForStandardize = -CustomTool.Vec2DegRotate(startTile.col, startTile.row, -riverDirAngle);

        Vector2 standardizedStart = CustomTool.Vec2DegRotate(startTile.col, startTile.row, -riverDirAngle) + deltaForStandardize;
        Vector2 standardizedEnd = CustomTool.Vec2DegRotate(endTile.col, endTile.row, -riverDirAngle) + deltaForStandardize;
        List<Vector2> standizedPointList = new List<Vector2>();
        float standardizedCol, standardizedRow;
        int pointNum = (int)Random.Range(Mathf.Sqrt(standardizedEnd.x), Mathf.Sqrt(standardizedEnd.x) * 1.5f);
        for (int i = 0; i < pointNum; ++i)    // 시작과 끝을 제외한 나머지를 정렬
        {
            standardizedCol = Random.Range(standardizedStart.x - Biome.size, standardizedEnd.x + Biome.size);
            standardizedRow = Random.Range(2, Biome.size);
            standardizedRow = CustomRandom.PickByProba(50) ? standardizedRow : -standardizedRow;

            Vector2 standardizedPoint = new Vector2(standardizedCol, standardizedRow);
            standizedPointList.Add(standardizedPoint);
        }
        standizedPointList.Sort((vec1, vec2) => { return vec1.x.CompareTo(vec2.x); });

        List<Vector2> detailRiverPoint = new List<Vector2>();
        detailRiverPoint.Add(new Vector2(startTile.transform.position.x, startTile.transform.position.y));
        foreach (Vector2 standizedPoint in standizedPointList)
        {
            Vector2 point = CustomTool.Vec2DegRotate(standizedPoint - deltaForStandardize, riverDirAngle);
            point.x = (int)point.x;
            point.y = (int)point.y;
            point.x = point.x * Tile.scale.x;
            point.y = point.y * Tile.scale.y;
            detailRiverPoint.Add(point);
        }
        detailRiverPoint.Add(new Vector2(endTile.transform.position.x, endTile.transform.position.y));

        return detailRiverPoint;
    }

    private void ConnectRiver(List<Tile> candiateTileList, List<float> lineEquation)
    {
        if (float.IsNaN(lineEquation[0]) || lineEquation.Count == 1)
        {
            foreach (Tile tile in candiateTileList)
                GenerateTile(tile, TileManager.TileKind.Water);
        }
        else
        {
            float x1, y1, x2, y2;
            foreach (Tile tile in candiateTileList)
            {
                x1 = tile.transform.position.x - Tile.scale.x / 2;
                y1 = CustomTool.GetYByLineEquation(lineEquation, x1);
                x2 = tile.transform.position.x + Tile.scale.x / 2;
                y2 = CustomTool.GetYByLineEquation(lineEquation, x2);
                float tilePosY = tile.transform.position.y;


                if ((tilePosY - Tile.scale.y / 2 < y1 && y1 <= tilePosY + Tile.scale.y / 2) ||
                    (tilePosY - Tile.scale.y / 2 < y2 && y2 <= tilePosY + Tile.scale.y / 2))
                {
                    GenerateTile(tile, TileManager.TileKind.Water);
                }
                else
                {
                    float y3 = tile.transform.position.y - Tile.scale.y / 2;
                    float x3 = CustomTool.GetXByLineEquation(lineEquation, y3);
                    float y4 = tile.transform.position.y + Tile.scale.y / 2;
                    float x4 = CustomTool.GetXByLineEquation(lineEquation, y4);
                    float tilePosX = tile.transform.position.x;
                    if (lineEquation[0] > 0)
                    {
                        if ((tilePosX - Tile.scale.x / 2 <= x3 && x3 < tilePosX + Tile.scale.x / 2) ||
                            (tilePosX - Tile.scale.x / 2 <= x4 && x4 < tilePosX + Tile.scale.x / 2))
                            GenerateTile(tile, TileManager.TileKind.Water);
                    }
                    else
                    {
                        if ((tilePosX - Tile.scale.x / 2 < x3 && x3 <= tilePosX + Tile.scale.x / 2) ||
                            (tilePosX - Tile.scale.x / 2 < x4 && x4 <= tilePosX + Tile.scale.x / 2))
                            GenerateTile(tile, TileManager.TileKind.Water);
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
