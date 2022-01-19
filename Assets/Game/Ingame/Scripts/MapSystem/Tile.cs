using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile neighbor;
    private float _fertility;
    private TileManager.TileKind _kind;
    public SpriteRenderer spriteRenderer;

    public virtual void ChangeKind(TileManager.TileInfo info)
    {
        spriteRenderer.sprite = info.sprite;
        _kind = info.kind;
    }
}
