using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    public enum kind
    {
        test1, test2
    };

    public Zone zone;
    public List<Tile> tileList;
    public Biome[] neighbor = new Biome[4];    // 0: N // 1: E // 2: S // 3: W

    [HideInInspector]
    public bool isEdge;

    
    [SerializeField]
    private int _col;
    [SerializeField]
    private int _row;
    private Dictionary<TileManager.TileKind, List<Tile>> _tileListAsKind;
    public int col { get { return _col; } private set { _col = value; } }
    public int row { get { return _row; } private set { _row = value; } }

    public static int size { get; private set; }
    public static int tileCount { get; private set; }

    private void Awake()
    {
        size = (int)Mathf.Sqrt(tileList.Count);
        tileCount = tileList.Count;
    }

    public void Init()
    {
        _tileListAsKind = new Dictionary<TileManager.TileKind, List<Tile>>();
        foreach (TileManager.TileKind kind in TileManager.GetEnumList<TileManager.TileKind>())
            _tileListAsKind.Add(kind, new List<Tile>());

        foreach (Tile tile in tileList)
            tile.Init();
    }

    public void SetCol(int col)
    {
        _col = col;
    }
    public void SetRow(int row)
    {
        _row = row;
    }
    public void SetTileAsKind(Tile tile, TileManager.TileKind kind)
    {
        TileManager.TileKind prevKind = tile.kind;

        if(prevKind != kind)
            _tileListAsKind[prevKind].Remove(tile);
        _tileListAsKind[kind].Add(tile);

        zone.SetTileCountAsKind(prevKind, kind);
    }

    public int GetColDistance(Biome other)
    {
        return Mathf.Abs(_col - other.col);
    }
    public int GetRowDistance(Biome other)
    {
        return Mathf.Abs(_row - other.row);
    }
    public IReadOnlyList<Tile> GetTileAsKind(TileManager.TileKind kind)
    {
        return _tileListAsKind[kind];
    }

    public List<Tile> GetEdgeTiles()
    {
        List<Tile> edgeTileList = new List<Tile>();

        foreach (Tile tile in tileList)
        {
            if (tile.isEdge)
                edgeTileList.Add(tile);
        }

        return edgeTileList;
    }

    public Tile FindTile(int tileCol, int tileRow)
    {
        int childCol = tileCol - col * size;
        int childRow = tileRow - row * size;

        if (childCol < 0 || childCol >= size ||
            childRow < 0 || childRow >= size)
        {
            Debug.Log("이 biome에 없는 tile입니다.");
            return null;
        }

        return tileList[childRow * size + childCol];
    }
}
