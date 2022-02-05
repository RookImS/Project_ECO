using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoSingleton<TileManager>
{
    public enum TileKind
    {
        None, Grass, Plain, Swamp, Mountain, Water, Hole
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

    public static Dictionary<TileKind, TileInfo> tileInfoDict;
    public static Dictionary<ClimateTileKind, ClimateTileInfo> climateTileInfoDict;

    private void Awake()
    {
        tileInfoDict = new Dictionary<TileKind, TileInfo>();
        climateTileInfoDict = new Dictionary<ClimateTileKind, ClimateTileInfo>();

        foreach (TileInfo info in tileTemplate)
            tileInfoDict.Add(info.kind, info);

        foreach (ClimateTileInfo info in climateTileTemplate)
            climateTileInfoDict.Add(info.kind, info);
    }

    // enum을 generic을 이용해 list로 변환해 반환한다.
    public static List<TEnum> GetEnumList<TEnum>() where TEnum : System.Enum
    {
        List<TEnum> enumList = System.Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();

        return enumList;
    }
}