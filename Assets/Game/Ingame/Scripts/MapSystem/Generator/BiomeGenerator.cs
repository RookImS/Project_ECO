using System.Collections;
using System.Collections.Generic;

public class BiomeGenerator
{
    private TileGenerator tileGenerator;
    private Dictionary<Biome, int> _notGenTileNum;

    public void Init(Map map)
    {
        tileGenerator = new TileGenerator();
        tileGenerator.Init(map);

        _notGenTileNum = new Dictionary<Biome, int>();
        foreach (Zone zone in map.zoneList)
        {
            foreach (Biome biome in zone.biomeList)
                _notGenTileNum.Add(biome, Biome.tileCount);
        }
    }

    public void SetStartTile(List<Biome> biomeList, TileManager.TileKind kind, int count)
    {
        List<int> notGenTileNum = new List<int>();
        foreach (Biome biome in biomeList)
            notGenTileNum.Add(_notGenTileNum[biome]);

        // 각 biome의 시작 타일 개수를 정함
        List<int> biomeStartTileNum = CustomRandom.RandomlyDistributeNumber(count, biomeList.Count, notGenTileNum);

        Biome tempBiome;
        List<Tile> incompleteTileList;
        for (int i = 0; i < biomeList.Count; ++i)
        {
            tempBiome = biomeList[i];
            incompleteTileList = tileGenerator.GetIncompleteTile(tempBiome.tileList);
            tileGenerator.SetStartTile(incompleteTileList, kind, biomeStartTileNum[i]);
        }
    }

    public List<Biome> GetIncompleteBiome(List<Biome> biomeList)
    {
        List<Biome> incompleteBiomeList = new List<Biome>();

        foreach (Biome biome in biomeList)
        {
            if (_notGenTileNum[biome] != 0)
                incompleteBiomeList.Add(biome);
        }

        return incompleteBiomeList;
    }

    public int CheckGenComplete(List<Biome> biomeList)
    {
        int notGenTileNum = 0;

        foreach (Biome biome in biomeList)
        {
            _notGenTileNum[biome] = tileGenerator.CheckGenComplete(biome.tileList);
            notGenTileNum += _notGenTileNum[biome];
        }

        return notGenTileNum;
    }
}
