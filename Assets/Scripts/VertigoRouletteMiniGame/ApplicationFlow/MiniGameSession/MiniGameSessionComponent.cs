using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.Inventory;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

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
        
        public IEnumerator Initialize(MiniGameConfiguration miniGameConfiguration)
        {
            this.miniGameConfiguration = miniGameConfiguration;
            this.obtainedRewardInventory = new PlayerInventory();
            status = EVictoryStatus.None;
            
            
            AsyncOperationHandle<ZoneMapConfigurationBase> loadAssetAsync = miniGameConfiguration.ZoneMapConfiguration.LoadAssetAsync<ZoneMapConfigurationBase>();
            yield return loadAssetAsync;

            if (loadAssetAsync.Result == null)
            {
                throw new System.Exception("Failed to load zone map configuration");
            }

            ZoneMapConfigurationBase zoneMapConfiguration = loadAssetAsync.Result;
            zoneMapInstance = new ZoneMapInstance(zoneMapConfiguration);
            
            yield return null;
        }


        public List<ZoneInstance> GetZoneInstances()
        {
            if (zoneMapInstance == null)
                return null;
            return zoneMapInstance.ZoneInstances;
        }
      
       
        
        
    }
}