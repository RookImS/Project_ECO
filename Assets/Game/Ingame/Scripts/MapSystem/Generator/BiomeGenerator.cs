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

    public void SetStartTile(Zone zone, TileManager.TileKind kind, int count)
    {
        // 아직 생성이 덜된 biome만을 다룸
        List<Biome> incompleteBiomeList = GetIncompleteBiome(zone);

        // 각 biome의 시작 타일 개수를 정함
        List<int> notGenTileNum = new List<int>();
        foreach (Biome biome in incompleteBiomeList)
            notGenTileNum.Add(_notGenTileNum[biome]);
        List<int> biomeStartTileNum = CustomRandom.RandomlyDistributeNumber(count, incompleteBiomeList.Count, notGenTileNum);

        Biome tempBiome;
        for (int i = 0; i < incompleteBiomeList.Count; ++i)
        {
            tempBiome = incompleteBiomeList[i];
            tileGenerator.SetStartTile(tempBiome, kind, biomeStartTileNum[i]);
        }
    }
    public void StretchTile(Zone zone, TileManager.TileKind kind, int proba, bool isCanOverlap)
    {
        foreach (Biome biome in zone.biomeList)
            tileGenerator.StretchTile(biome, kind, proba, isCanOverlap);
    }

    public List<Biome> GetIncompleteBiome(Zone zone)
    {
        List<Biome> incompleteBiomeList = new List<Biome>();

        foreach (Biome biome in zone.biomeList)
        {
            if (_notGenTileNum[biome] != 0)
                incompleteBiomeList.Add(biome);
        }

        return incompleteBiomeList;
    }

    public int CheckGenComplete(Zone zone)
    {
        int notGenTileNum = 0;

        foreach (Biome biome in zone.biomeList)
        {
            _notGenTileNum[biome] = tileGenerator.CheckGenComplete(biome);
            notGenTileNum += _notGenTileNum[biome];
        }

        return notGenTileNum;
    }
}
