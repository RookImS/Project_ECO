using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoSingleton<TileManager>
{
    public enum TileKind
    {
        Grass, Plain, Swamp, Mountain, Water, Hole
    };

    public enum TileState
    {
        Weak, Normal
    };

    public enum ClimateTileKind
    {
        Desert, Tundra
    };

    [System.Serializable]
    public class TileInfo
    {
        public Sprite sprite;
        public TileKind kind;
        public TileState state;
        public float threshold;
    }

    [System.Serializable]
    public class ClimateTileInfo
    {
        public Sprite sprite;
        public ClimateTileKind kind;
    }

    public List<TileInfo> tileTemplate;
    public List<ClimateTileInfo> climateTileTemplate;

    public static List<TileInfo> tileInfoList;
    public static List<ClimateTileInfo> climateTileInfoList;

    private void Awake()
    {
        tileInfoList = new List<TileInfo>();
        climateTileInfoList = new List<ClimateTileInfo>();
        tileInfoList = tileTemplate.ToList();
        climateTileInfoList = climateTileTemplate.ToList();
    }
}