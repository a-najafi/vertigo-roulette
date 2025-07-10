using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession
{
    [CreateAssetMenu(fileName = "New Roulette Configuration", menuName = "MiniGameSession/Roulette Configuration")]
    public class RouletteConfigurationFixed : RouletteConfigurationBase
    {
        [SerializeField]private List<RouletteRewardConfiguration> _rouletteRewardConfigurations = new List<RouletteRewardConfiguration>(); 
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