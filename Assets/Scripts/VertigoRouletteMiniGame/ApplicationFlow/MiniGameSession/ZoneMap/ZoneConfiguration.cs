using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap
{
    
    [CreateAssetMenu(fileName = "ZoneConfiguration", menuName = "MiniGame/ZoneConfiguration")]
    public class ZoneConfiguration : ScriptableObject
    {
        
        [SerializeField]private AssetReferenceAtlasedSprite _rouletteSprite;
        [SerializeField]private AssetReferenceAtlasedSprite _roulettePinSprite;
        
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteComing;
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteActive;
        [SerializeField]private AssetReferenceAtlasedSprite _zoneSpriteResolved;
        [SerializeField]private AssetReference _rouletteConfigAsset;
        
        public AssetReferenceAtlasedSprite RouletteSprite => _rouletteSprite;

        public AssetReferenceAtlasedSprite RoulettePinSprite => _roulettePinSprite;


        public AssetReferenceAtlasedSprite ZoneSpriteComing => _zoneSpriteComing;

        public AssetReferenceAtlasedSprite ZoneSpriteActive => _zoneSpriteActive;

        public AssetReferenceAtlasedSprite ZoneSpriteResolved => _zoneSpriteResolved;

        public AssetReference RouletteConfigAsset => _rouletteConfigAsset;
    }
}