using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap
{
    /// <summary>
    /// Configuration ScriptableObject for a single zone in the roulette minigame.
    /// Holds references to all sprites and the roulette configuration asset.
    /// </summary>
    [CreateAssetMenu(fileName = "ZoneConfiguration", menuName = "MiniGame/ZoneConfiguration")]
    public class ZoneConfiguration : ScriptableObject
    {
        #region Serialized Parameters

        [Header("Roulette Visuals")]
        [SerializeField] private AssetReferenceAtlasedSprite _rouletteSprite;
        [SerializeField] private AssetReferenceAtlasedSprite _roulettePinSprite;

        [Header("Zone State Sprites")]
        [SerializeField] private AssetReferenceAtlasedSprite _zoneSpriteComing;
        [SerializeField] private AssetReferenceAtlasedSprite _zoneSpriteActive;
        [SerializeField] private AssetReferenceAtlasedSprite _zoneSpriteResolved;

        [Header("Roulette Config Reference")]
        [SerializeField] private AssetReference _rouletteConfigAsset;

        #endregion

        #region Properties

        /// <summary>Sprite for the roulette UI.</summary>
        public AssetReferenceAtlasedSprite RouletteSprite => _rouletteSprite;

        /// <summary>Pin/needle sprite for the roulette.</summary>
        public AssetReferenceAtlasedSprite RoulettePinSprite => _roulettePinSprite;

        /// <summary>Sprite when the zone is in 'Coming Soon' state.</summary>
        public AssetReferenceAtlasedSprite ZoneSpriteComing => _zoneSpriteComing;

        /// <summary>Sprite for the currently active zone.</summary>
        public AssetReferenceAtlasedSprite ZoneSpriteActive => _zoneSpriteActive;

        /// <summary>Sprite when the zone is resolved/cleared.</summary>
        public AssetReferenceAtlasedSprite ZoneSpriteResolved => _zoneSpriteResolved;

        /// <summary>Reference to the roulette rewards/config asset.</summary>
        public AssetReference RouletteConfigAsset => _rouletteConfigAsset;

        #endregion
    }
}
