using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap
{
    public enum EZoneType
    {
        None = 0,
        Normal = 1,
        Special = 2,
        SuperSpecial = 3,
    }
    
    public class ZoneConfiguration : ScriptableObject
    {
        [SerializeField]private EZoneType _zoneType;
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteComing;
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteActive;
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteResolved;
        [SerializeField]private AssetReference _rouletteConfigAsset;
    }
}