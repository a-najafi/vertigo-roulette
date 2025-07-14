using System.Collections;
using BasicSM;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    public class SpinRoulette : MiniGameSessionStateComponentBase
    {
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            yield return miniGameSessionComponent.SpinRoulette();
            
            if(miniGameSessionComponent.GetActiveZoneInstance().ZoneState != EZoneState.Win)
                yield break;
            
            RouletteRewardConfiguration rewardConfiguration = miniGameSessionComponent.GetActiveZoneInstance()
                .RouletteInstance.ResultRewardConfiguration;
            miniGameSessionComponent.AddReward(rewardConfiguration);
            
            
        }
    }
}