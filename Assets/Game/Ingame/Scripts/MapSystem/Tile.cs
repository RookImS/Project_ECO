using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Biome biome;
    public Tile[] neighbor = new Tile[4];  // 0: N // 1: E // 2: S // 3: W

    public TileManager.TileKind kind;

    [HideInInspector]
    public bool isEdge;

    [SerializeField]
    private int _col;
    [SerializeField]
    private int _row;
    public int col { get { return _col; } private set { _col = value; } }
    public int row { get { return _row; } private set { _row = value; } }

    public static Vector2 scale { get; private set; }

    protected float _fertility;
    private int _height;

    public void SetCol(int col)
    {
        _col = col;
    }
    public void SetRow(int row)
    {
        _row = row;
    }

    public virtual void Init()
    {
        scale = new Vector2(transform.lossyScale.x, transform.lossyScale.y);
        _fertility = 0;
        ChangeKind(TileManager.TileKind.None);
    }

    public int GetColDistance(Tile other)
    {
        return Mathf.Abs(_col - other.col);
    }
    public int GetRowDistance(Tile other)
    {
        return Mathf.Abs(_row - other.row);
    }
    public float GetColRowDistance(Tile other)
    {
        
        return Mathf.Sqrt(Mathf.Pow(GetColDistance(other), 2) + Mathf.Pow(GetRowDistance(other), 2));
    }    

    public bool isInTile(Vector2 pos)
    {
        Vector2 tilePos = new Vector2(transform.position.x, transform.position.y);

        if ((tilePos.x - scale.x / 2 <= pos.x) && (pos.x < tilePos.x + scale.x / 2) &&
            (tilePos.y - scale.y / 2 <= pos.y) && (pos.y < tilePos.y + scale.y / 2))
            return true;
        else
            return false;
    }

    public virtual void SetTile(TileManager.TileKind kind)
    {
        _fertility = 40;    // 나중에 산출 방법 정해야함
        ChangeKind(kind);
    }

    public void CalcHeight(int change)
    {
        _height += change;

        if (_height < 1)
            _height = 1;
        else if (_height > 7)
            _height = 7;
    }

    protected virtual void ChangeKind(TileManager.TileKind kind)
    {
        spriteRenderer.sprite = TileManager.tileInfoDict[kind].sprite;

        biome.SetTileAsKind(this, kind);
        this.kind = TileManager.tileInfoDict[kind].kind;
    }

    public static int XToCol(float x)
    {
        return (int)(x / scale.x);
    }
    public static int YToRow(float y)
    {
        return (int)(y / scale.y);
    }
    public static Vector2 PosToColRow(float x, float y)
    {
        return new Vector2(XToCol(x), YToRow(y));
    }
    public static Vector2 PosToColRow(Vector2 pos)
    {
        return PosToColRow(pos.x, pos.y);
    }
    public static Vector2 PosToColRow(Vector3 pos)
    {
        return PosToColRow(pos.x, pos.y);
    }
    public static float ColToX(int col)
    {
        return col * scale.x;
    }
    public static float RowToY(int row)
    {
        return row * scale.y;
    }
    public static Vector2 ColRowToPos(int col, int row)
    {
        return new Vector2(ColToX(col), RowToY(row));
    }
}
