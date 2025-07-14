using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    public class SpinRoulette : MiniGameSessionStateComponentBase
    {
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            ZoneInstance activeZone = miniGameSessionComponent.GetActiveZoneInstance();
            yield return miniGameSessionComponent.SpinRoulette();
            
            if(activeZone.ZoneState != EZoneState.Win)
                yield break;
            
            RouletteRewardConfiguration rewardConfiguration = activeZone.RouletteInstance.ResultRewardConfiguration;
            Debug.Log("reward obtained " + activeZone
                .RouletteInstance.ResultIndex + " reward item " + activeZone
                .RouletteInstance.ResultRewardConfiguration.ItemDefinition.AssetGUID + " count " + activeZone.RouletteInstance.ResultRewardConfiguration.Amount);
            miniGameSessionComponent.AddReward(rewardConfiguration);
            
            
        }
    }
}