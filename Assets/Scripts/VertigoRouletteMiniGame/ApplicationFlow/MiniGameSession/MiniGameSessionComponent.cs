using System.Collections;
using UnityEngine;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession
{
    /// <summary>
    /// Component that manages the lifecycle and state of a mini-game session,
    /// including the player's obtained rewards, zone progression, and victory status.
    /// </summary>
    public enum EVictoryStatus
    {
        None = 0,
        LeaveWithPrize = 1,
        LoseEverything = 2,
    }

    public class MiniGameSessionComponent : MonoBehaviour
    {
        #region Non-Serialized Fields

        /// <summary>
        /// Temporary inventory holding rewards the player can obtain during the session.
        /// </summary>
        private PlayerInventory obtainedRewardInventory = new PlayerInventory();

        /// <summary>
        /// The configuration data for this mini-game session.
        /// </summary>
        private MiniGameConfiguration miniGameConfiguration;

        /// <summary>
        /// The current session's victory status.
        /// </summary>
        private EVictoryStatus status = EVictoryStatus.None;

        /// <summary>
        /// The instance managing zone state and progression.
        /// </summary>
        private ZoneMapInstance zoneMapInstance;

        #endregion

        #region Properties

        /// <summary>
        /// Rewards obtained so far during this session.
        /// </summary>
        public PlayerInventory ObtainedRewardInventory => obtainedRewardInventory;

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the current zone map instance.
        /// </summary>
        public ZoneMapInstance GetZoneMapInstance()
        {
            return zoneMapInstance;
        }

        /// <summary>
        /// Get the currently active zone instance.
        /// </summary>
        public ZoneInstance GetActiveZoneInstance()
        {
            return zoneMapInstance.GetActiveZoneInstance();
        }

        /// <summary>
        /// Initializes the session with the specified configuration, loads zones and prepares first roulette.
        /// </summary>
        public IEnumerator Initialize(MiniGameConfiguration miniGameConfiguration)
        {
            this.miniGameConfiguration = miniGameConfiguration;
            this.obtainedRewardInventory = new PlayerInventory();
            status = EVictoryStatus.None;

            // Load the zone map configuration as an addressable asset
            AssetLoadResult<ZoneMapConfigurationBase> loadedZoneMapConfiguration = new AssetLoadResult<ZoneMapConfigurationBase>();
            yield return AddressableAssetManager.LoadAsset<ZoneMapConfigurationBase>(miniGameConfiguration.ZoneMapConfiguration, loadedZoneMapConfiguration);

            if (loadedZoneMapConfiguration.Asset == null)
            {
                throw new System.Exception("Failed to load zone map configuration");
            }

            ZoneMapConfigurationBase zoneMapConfiguration = loadedZoneMapConfiguration.Asset;
            zoneMapInstance = new ZoneMapInstance(zoneMapConfiguration);

            // Initialize the first zone's roulette instance
            yield return zoneMapInstance.GetActiveZoneInstance().InitializeRouletteInstance();
            yield return null;
        }

        /// <summary>
        /// Spins the current zone's roulette. Advances to the next zone if eligible.
        /// </summary>
        public IEnumerator SpinRoulette()
        {
            ZoneInstance zoneInstance = zoneMapInstance.GetActiveZoneInstance();
            yield return zoneInstance.SpinRouletteZone();
            if (zoneMapInstance.TryMakeNextZoneActive())
            {
                zoneInstance = zoneMapInstance.GetActiveZoneInstance();
                yield return zoneInstance.InitializeRouletteInstance();
            }
        }

        /// <summary>
        /// Add a reward to the player's temporary obtained inventory for this session.
        /// </summary>
        public void AddReward(RouletteRewardConfiguration rouletteRewardConfiguration)
        {
            obtainedRewardInventory.IncreaseCount(rouletteRewardConfiguration.ItemDefinition.AssetGUID, rouletteRewardConfiguration.Amount);
        }

        #endregion
    }
}
