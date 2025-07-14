using System;
using System.Collections;
using System.Collections.Generic;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession
{
    public enum ERouletteState
    {
        None = 0,
        Resolved = 1
    }
    public class RouletteInstance 
    {
        private RouletteConfigurationBase configuration;
        private List<RouletteRewardConfiguration> rewardConfigurations;
        private ERouletteState state;
        private int resultIndex = -1;

        public int ResultIndex => resultIndex;


        public RouletteConfigurationBase Configuration => configuration;

        public List<RouletteRewardConfiguration> RewardConfigurations => rewardConfigurations;

        public ERouletteState State => state;

        public RouletteRewardConfiguration ResultRewardConfiguration
        {
            get
            {
                if (resultIndex < 0 || resultIndex >= rewardConfigurations.Count)
                    return null;
                return rewardConfigurations[resultIndex];
            }
        }


        public RouletteInstance(RouletteConfigurationBase rouletteConfigurationBase)
        {
            configuration = rouletteConfigurationBase;
            rewardConfigurations = configuration.GetRewardConfigurations();
            state = ERouletteState.None;
            
        }
        
        
        public void SpinRoulette()
        {
            
            
            int rewardCount = rewardConfigurations.Count;
            float totalRewardWeight = 0;
            float[] weightCaps = new float[rewardCount];
            for (int i = 0; i < rewardCount; i++)
            {
                totalRewardWeight += 1 + rewardConfigurations[i].ProbabilityModifier;
                weightCaps[i] = totalRewardWeight;
            }
            float random = UnityEngine.Random.Range(0, totalRewardWeight);
            int randomIndex = -1;
            for (int i = 0; i < rewardCount; i++)
            {
                if (random < weightCaps[i])
                {
                    randomIndex = i;
                    break;
                }
            }
            
            if(randomIndex < 0)
                throw new System.Exception("Random index out of range");
            
            SetResolved(randomIndex);
            
        }
        

        public void SetResolved(int index)
        {
            if(index <0 || index >= rewardConfigurations.Count)
                throw new Exception("Invalid reward index");
            
            this.resultIndex = index; 
            state = ERouletteState.Resolved;
            
        }
        
    }
}