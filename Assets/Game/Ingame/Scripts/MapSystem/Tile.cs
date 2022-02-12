using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Biome biome;

    public int row;
    public int col;
    public Tile[] neighbor = new Tile[4];  // 0: N // 1: E // 2: S // 3: W

    public TileManager.TileKind kind;

    [HideInInspector]
    public bool isEdge;

    protected float _fertility;
    private int _height;

    public virtual void Init()
    {
        _fertility = 0;
        ChangeKind(TileManager.TileKind.None);
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
        this.kind = TileManager.tileInfoDict[kind].kind;
    }

    public int GetRowDistance(Tile other)
    {
        return Mathf.Abs(row - other.row);
    }

    public int GetColDistance(Tile other)
    {
        return Mathf.Abs(col - other.col);
    }
}
