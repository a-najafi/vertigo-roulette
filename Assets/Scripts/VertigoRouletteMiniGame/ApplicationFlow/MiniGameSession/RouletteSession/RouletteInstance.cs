using System.Collections.Generic;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession
{
    public enum ERouletteState
    {
        Initializing = 0,
        Begin = 1,
        Rolling = 2,
        End = 3,
    }
    public class RouletteInstance 
    {
        private RouletteConfigurationBase configuration;
        private List<RouletteRewardConfiguration> rewardConfigurations;
        private ERouletteState state;
        private RouletteRewardConfiguration resultRewardConfiguration;
        
        public RouletteConfigurationBase Configuration => configuration;

        public List<RouletteRewardConfiguration> RewardConfigurations => rewardConfigurations;

        public ERouletteState State => state;

        public RouletteRewardConfiguration ResultRewardConfiguration => resultRewardConfiguration;

        
    }
}