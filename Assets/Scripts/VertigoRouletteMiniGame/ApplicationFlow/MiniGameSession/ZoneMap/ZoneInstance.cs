using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{

    public enum EZoneState
    {
        None,
        Win,
        Lose
    }

    public class ZoneInstance
    {
        private int zoneIndex;
        private ZoneConfiguration zoneConfiguration;
        private EZoneState zoneState;
        private RouletteInstance rouletteInstance = null;

        public int ZoneIndex => zoneIndex;

        public ZoneConfiguration ZoneConfiguration => zoneConfiguration;

        public EZoneState ZoneState => zoneState;

        public RouletteInstance RouletteInstance => rouletteInstance;


        public ZoneInstance(int zoneIndex, ZoneConfiguration zoneConfiguration)
        {
            this.zoneIndex = zoneIndex;
            this.zoneConfiguration = zoneConfiguration;
            this.zoneState = EZoneState.None;
        }

        public IEnumerator SpinRouletteZone()
        {
            if(rouletteInstance == null)
                yield return InitializeRouletteInstance();
            
            rouletteInstance.SpinRoulette();
            if (rouletteInstance.ResultRewardConfiguration.IsBomb)
                zoneState = EZoneState.Lose;
            else
                zoneState = EZoneState.Win;
        }

        public IEnumerator InitializeRouletteInstance()
        {
            yield return AddressableAssetManager.LoadAsset<RouletteConfigurationBase>(zoneConfiguration.RouletteConfigAsset,
                rouletteConfig =>
                {
                    rouletteInstance = new RouletteInstance(rouletteConfig);
                });
        }
        
      
    }
}