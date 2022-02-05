using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertibleTile : Tile
{
    private TileManager.TileState _state;
    private float _threshold;

    protected override void ChangeKind(TileManager.TileKind kind)
    {
        base.ChangeKind(kind);
        _state = TileManager.tileInfoDict[kind].state;
        _threshold = TileManager.tileInfoDict[kind].threshold;
    }
}
