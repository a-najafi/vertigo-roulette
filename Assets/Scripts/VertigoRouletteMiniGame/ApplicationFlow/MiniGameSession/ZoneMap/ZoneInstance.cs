using System.Collections;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    /// <summary>
    /// Holds the runtime state for a single zone in the roulette minigame.
    /// Handles spinning, state transitions, and roulette instance setup.
    /// </summary>
    public enum EZoneState
    {
        None,
        Win,
        Lose
    }

    public class ZoneInstance
    {
        #region Non-Serialized Parameters

        /// <summary>Zone index in the overall map.</summary>
        private int zoneIndex;
        /// <summary>Reference to configuration asset.</summary>
        private ZoneConfiguration zoneConfiguration;
        /// <summary>Current state of the zone.</summary>
        private EZoneState zoneState;
        /// <summary>Current roulette instance for this zone.</summary>
        private RouletteInstance rouletteInstance = null;

        #endregion

        #region Properties

        /// <summary>Index of this zone instance.</summary>
        public int ZoneIndex => zoneIndex;

        /// <summary>ScriptableObject configuration for this zone.</summary>
        public ZoneConfiguration ZoneConfiguration => zoneConfiguration;

        /// <summary>Current zone state (None/Win/Lose).</summary>
        public EZoneState ZoneState => zoneState;

        /// <summary>RouletteInstance for this zone (created on demand).</summary>
        public RouletteInstance RouletteInstance => rouletteInstance;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new ZoneInstance with the specified configuration and index.
        /// </summary>
        public ZoneInstance(int zoneIndex, ZoneConfiguration zoneConfiguration)
        {
            this.zoneIndex = zoneIndex;
            this.zoneConfiguration = zoneConfiguration;
            this.zoneState = EZoneState.None;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Spins the roulette for this zone and sets the zone state based on the result.
        /// </summary>
        public IEnumerator SpinRouletteZone()
        {
            // Ensure a roulette instance exists.
            if (rouletteInstance == null)
                yield return InitializeRouletteInstance();

            rouletteInstance.SpinRoulette();

            // If the result is a bomb, the zone is lost, otherwise it's a win.
            if (rouletteInstance.ResultRewardConfiguration.IsBomb)
                zoneState = EZoneState.Lose;
            else
                zoneState = EZoneState.Win;
        }

        /// <summary>
        /// Loads the roulette configuration and initializes the roulette instance.
        /// </summary>
        public IEnumerator InitializeRouletteInstance()
        {
            var result = new AssetLoadResult<RouletteConfigurationBase>();
            yield return AddressableAssetManager.LoadAsset<RouletteConfigurationBase>(
                zoneConfiguration.RouletteConfigAsset, result);

            rouletteInstance = new RouletteInstance(result.Asset);
        }

        #endregion
    }
}
