using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap
{
    public class ZoneConfiguration : ScriptableObject
    {
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteComing;
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteActive;
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteResolved;
        [SerializeField]private AssetReference _rouletteConfigAsset;
    }
}