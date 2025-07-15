using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;
using Random = UnityEngine.Random;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession
{
    public enum EVictoryStatus
    {
        None = 0,
        LeaveWithPrize = 1,
        LoseEverything = 2,
    }
    public class MiniGameSessionComponent : MonoBehaviour
    {
        private PlayerInventory obtainedRewardInventory = new PlayerInventory();
        private MiniGameConfiguration miniGameConfiguration;
        private EVictoryStatus status = EVictoryStatus.None;
        private ZoneMapInstance zoneMapInstance;
        
        public PlayerInventory ObtainedRewardInventory => obtainedRewardInventory;
        public ZoneMapInstance GetZoneMapInstance()
        {
            return zoneMapInstance;
        }

        public ZoneInstance GetActiveZoneInstance()
        {
            return zoneMapInstance.GetActiveZoneInstance();
        }

        
        public IEnumerator Initialize(MiniGameConfiguration miniGameConfiguration)
        {
            this.miniGameConfiguration = miniGameConfiguration;
            this.obtainedRewardInventory = new PlayerInventory();
            status = EVictoryStatus.None;
            
            AssetLoadResult<ZoneMapConfigurationBase> loadedZoneMapConfiguration = new AssetLoadResult<ZoneMapConfigurationBase>();
            yield return AddressableAssetManager.LoadAsset<ZoneMapConfigurationBase>(miniGameConfiguration.ZoneMapConfiguration, loadedZoneMapConfiguration);

            if (loadedZoneMapConfiguration.Asset == null)
            {
                throw new System.Exception("Failed to load zone map configuration");
            }

            ZoneMapConfigurationBase zoneMapConfiguration = loadedZoneMapConfiguration.Asset;
            zoneMapInstance = new ZoneMapInstance(zoneMapConfiguration);

            yield return zoneMapInstance.GetActiveZoneInstance().InitializeRouletteInstance();
            
            yield return null;
        }

      
      
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

        public void AddReward(RouletteRewardConfiguration rouletteRewardConfiguration)
        {
            obtainedRewardInventory.IncreaseCount(rouletteRewardConfiguration.ItemDefinition.AssetGUID,rouletteRewardConfiguration.Amount);
        }
       
        
        
    }
}