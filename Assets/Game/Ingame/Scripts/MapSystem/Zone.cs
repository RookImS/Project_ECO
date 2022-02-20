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
    private int _col;
    [SerializeField]
    private int _row;
    public int col { get { return _col; } private set { _col = value; } }
    public int row { get { return _row; } private set { _row = value; } }

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

    public void SetCol(int col)
    {
        _col = col;
    }
    public void SetRow(int row)
    {
        _row = row;
    }

    public int GetColDistance(Zone other)
    {
        return Mathf.Abs(_col - other.col);
    }
    public int GetRowDistance(Zone other)
    {
        return Mathf.Abs(_row - other.row);
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

    public Tile FindTile(int tileCol, int tileRow)
    {
        int childCol = tileCol / Biome.size - col * size;
        int childRow = tileRow / Biome.size - row * size;

        if (childCol < 0 || childCol >= size || 
            childRow < 0 || childRow >= size)
        {
            Debug.Log("이 zone에 없는 tile입니다.");
            return null;
        }

        return biomeList[childRow * size + childCol].FindTile(tileCol, tileRow);
    }
}
