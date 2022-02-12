using System.Collections;
using System.Collections.Generic;

public class BiomeGenerator
{
    private TileGenerator _tileGenerator;
    private Dictionary<Biome, int> _notGenTileNum;

    public void Init(Map map)
    {
        _tileGenerator = new TileGenerator();
        _tileGenerator.Init(map);

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
        List<int> biomeStartTileNum = CustomRandom.DistributeNumber(count, incompleteBiomeList.Count, notGenTileNum);

        Biome tempBiome;
        for (int i = 0; i < incompleteBiomeList.Count; ++i)
        {
            tempBiome = incompleteBiomeList[i];
            _tileGenerator.SetStartTile(tempBiome, kind, biomeStartTileNum[i]);
        }
    }
    public void StretchTile(Zone zone, MapSetting.TileSetting tileSetting, bool isCanOverlap)
    {
        foreach (Biome biome in zone.biomeList)
            _tileGenerator.StretchTile(biome, tileSetting, isCanOverlap);
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
            _notGenTileNum[biome] = _tileGenerator.CheckGenComplete(biome);
            notGenTileNum += _notGenTileNum[biome];
        }

        return notGenTileNum;
    }
}
