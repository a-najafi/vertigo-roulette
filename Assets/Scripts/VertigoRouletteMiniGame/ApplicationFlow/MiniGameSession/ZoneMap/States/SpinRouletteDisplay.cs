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
            ZoneDisplayComponent activeZoneDisplayComponent = _zoneMapDisplayComponent.GetActiveZoneDisplay();
//            Debug.Log("SpinRouletteDisplay entered " + activeZoneDisplayComponent.ZoneInstance.RouletteInstance.ResultIndex);
            yield return activeZoneDisplayComponent
                .SpinRoulette(activeZoneDisplayComponent.ZoneInstance.RouletteInstance.ResultIndex);

            yield return activeZoneDisplayComponent.DisplayFinalReward();
        }
    }
}