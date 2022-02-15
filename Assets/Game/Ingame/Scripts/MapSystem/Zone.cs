using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public Map map;
    public List<Biome> biomeList;
    public Zone[] neighbor = new Zone[4];  // 0: N // 1: E // 2: S // 3: W

    [HideInInspector]
    public bool isEdge;

    [SerializeField]
    private int _row;
    [SerializeField]
    private int _col;
    public int row { get { return _row; } private set { _row = value; } }
    public int col { get { return _col; } private set { _col = value; } }

    public static int size { get; private set; }
    public static int biomeCount { get; private set; }

    private void Awake()
    {
        size = (int)Mathf.Sqrt(biomeList.Count);
        biomeCount = biomeList.Count;    
    }

    public void Init()
    {
        foreach (Biome biome in biomeList)
            biome.Init();
    }

    public void SetRow(int row)
    {
        _row = row;
    }
    public void SetCol(int col)
    {
        _col = col;
    }

    public int GetRowDistance(Zone other)
    {
        return Mathf.Abs(_row - other.row);
    }
    public int GetColDistance(Zone other)
    {
        return Mathf.Abs(_col - other.col);
    }

    public List<Tile> GetEdgeTiles()
    {
        List<Tile> edgeTileList = new List<Tile>();

        foreach (Biome biome in biomeList)
        {
            if (biome.isEdge)
                edgeTileList.AddRange(biome.GetEdgeTiles());
        }

        return edgeTileList;
    }

    public Tile FindTile(int tileRow, int tileCol)
    {
        int childRow = tileRow / Biome.size - row * size;
        int childCol = tileCol / Biome.size - col * size;

        if (childRow < 0 || childRow >= size ||
            childCol < 0 || childCol >= size)
        {
            Debug.Log("이 zone에 없는 tile입니다.");
            return null;
        }

        return biomeList[childRow * size + childCol].FindTile(tileRow, tileCol);
    }
}
