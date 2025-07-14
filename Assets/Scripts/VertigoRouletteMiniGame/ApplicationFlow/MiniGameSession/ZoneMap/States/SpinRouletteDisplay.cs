using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    public class SpinRouletteDisplay : MiniGameSessionStateComponentBase
    {
        [SerializeField] private ZoneMapDisplayComponent _zoneMapDisplayComponent;
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            yield return _zoneMapDisplayComponent.GetActiveZoneDisplay()
                .SpinRoulette(miniGameSessionComponent.GetActiveZoneInstance().RouletteInstance.ResultIndex);

            yield return _zoneMapDisplayComponent.GetActiveZoneDisplay().DisplayFinalReward();
        }
    }
}