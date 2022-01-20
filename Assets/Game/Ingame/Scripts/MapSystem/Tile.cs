using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Tile[] _neighbor = new Tile[4];  // 0: N // 1: W // 2: S // 3: E
    private float _fertility;
    private TileManager.TileKind _kind;

    public virtual void ChangeKind(TileManager.TileInfo info)
    {
        spriteRenderer.sprite = info.sprite;
        _kind = info.kind;
    }
}
