using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession
{
    /// <summary>
    /// ScriptableObject that configures a mini-game session,
    /// specifying the zone map configuration and maximum progression limit.
    /// </summary>
    [CreateAssetMenu(fileName = "MiniGameConfiguration", menuName = "MiniGame/MiniGameConfiguration")]
    public class MiniGameConfiguration : ScriptableObject
    {
        #region Serialized Parameters

        [SerializeField] private AssetReference _zoneMapConfigurationAssetReference;
        [SerializeField] private int _maximumProgression = -1;

        #endregion

        #region Properties

        /// <summary>
        /// The maximum progression allowed in the mini-game (-1 for unlimited).
        /// </summary>
        public int MaximumProgression => _maximumProgression;

        /// <summary>
        /// AssetReference to the ZoneMapConfiguration used by this mini-game.
        /// </summary>
        public AssetReference ZoneMapConfiguration => _zoneMapConfigurationAssetReference;

        #endregion
    }
}