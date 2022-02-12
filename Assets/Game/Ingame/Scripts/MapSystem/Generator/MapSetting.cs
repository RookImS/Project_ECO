using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetting : MonoBehaviour
{
    [System.Serializable]
    public struct TileSetting
    {
        [Tooltip("맵을 형성할 타일의 종류")]
        public TileManager.TileKind kind;
        [Tooltip("시작 타일이 있는 경우 시작 타일의 최소 개수")]
        public int minStart;
        [Tooltip("시작 타일이 있는 경우 시작 타일의 최대 개수")]
        public int maxStart;
        [Tooltip("시작 타일로부터 가지를 뻗는 형태로 맵을 만들 경우 주변으로 가지를 뻗을 확률 (%)")]
        [Range(0, 100)]
        public int stretchProba;
        [Tooltip("이미 가지를 뻗은 타일로부터 주변으로 타일이 퍼질 확률 (%)")]
        [Range(0, 100)]
        public int sprawlProba;

    }

    public List<TileSetting> tileSettings;

    public void Init()
    {
        // 값이 안맞는 것을 초기화
    }

    public TileSetting? GetTileSetting(TileManager.TileKind kind)
    {
        TileSetting result;

        foreach (TileSetting tileSetting in tileSettings)
        {
            if (tileSetting.kind == kind)
            {
                result = tileSetting;
                return result;
            }
        }

        Debug.LogError(kind + "에 대한 설정을 찾을 수 없습니다. " + ToString() + "의 설정을 확인하세요.");
        return null;
    }
}
