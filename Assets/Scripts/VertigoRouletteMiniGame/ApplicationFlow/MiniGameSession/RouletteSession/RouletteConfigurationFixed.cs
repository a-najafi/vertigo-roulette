using System;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession
{
    [CreateAssetMenu(fileName = "RouletteConfigurationFixed", menuName = "MiniGame/RouletteConfigurationFixed")]
    public class RouletteConfigurationFixed : RouletteConfigurationBase
    {
        [SerializeField]private List<RouletteRewardConfiguration> _rouletteRewardConfigurations = new List<RouletteRewardConfiguration>();

        #if UNITY_EDITOR
        private void OnValidate()
        {
            int rewardCount = _rouletteRewardConfigurations.Count;
            float totalRewardWeight = 0;
            float[] weightCaps = new float[rewardCount];
            for (int i = 0; i < rewardCount; i++)
            {
                 weightCaps[i] += 1 + _rouletteRewardConfigurations[i].ProbabilityModifier;
                 totalRewardWeight += weightCaps[i];
            }

            for (int i = 0; i < rewardCount; i++)
            {
                _rouletteRewardConfigurations[i].occurenceChance = weightCaps[i] / totalRewardWeight * 100;
            }
        }
        #endif

        public override List<RouletteRewardConfiguration> GetRewardConfigurations(int maxAmount = -1)
        {
            if(maxAmount < 0  )
                return _rouletteRewardConfigurations;
            else if(_rouletteRewardConfigurations.Count < maxAmount)
                return _rouletteRewardConfigurations;
            else
            {
                List<RouletteRewardConfiguration> truncatedRouletteRewardConfigurations = new List<RouletteRewardConfiguration>();
                for (int i = 0; i < maxAmount; i++)
                {
                    truncatedRouletteRewardConfigurations.Add(_rouletteRewardConfigurations[i]);
                }
                return truncatedRouletteRewardConfigurations;
            }
            
        }
    }
}