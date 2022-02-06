using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public int id;
    public Biome biome;
    public Tile[] neighbor = new Tile[4];  // 0: N // 1: E // 2: S // 3: W

    protected float _fertility;
    protected TileManager.TileKind _kind;

    private bool isGenerated;

    public virtual void Init()
    {
        _fertility = 0;
        ChangeKind(TileManager.TileKind.None);
        isGenerated = false;
    }

    public bool CheckIsGenerated()
    {
        return isGenerated;
    }

    public virtual void SetTile(TileManager.TileKind kind)
    {
        _fertility = 40;    // ���߿� ���� ��� ���ؾ���
        ChangeKind(kind);
    }

    protected virtual void ChangeKind(TileManager.TileKind kind)
    {
        spriteRenderer.sprite = TileManager.tileInfoDict[kind].sprite;
        _kind = TileManager.tileInfoDict[kind].kind;
    }

    
}
