using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession
{
    
    public class RouletteConfigurationBase : ScriptableObject
    {
        public virtual List<RouletteRewardConfiguration> GetRewardConfigurations(int maxAmount = -1)
        {
            throw new System.NotImplementedException();
        }

    }
}