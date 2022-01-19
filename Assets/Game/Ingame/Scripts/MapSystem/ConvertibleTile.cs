using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertibleTile : Tile
{
    private TileManager.TileState _state;
    private float _threshold;

    public override void ChangeKind(TileManager.TileInfo info)
    {
        base.ChangeKind(info);
        _state = info.state;
        _threshold = info.threshold;
    }
}
