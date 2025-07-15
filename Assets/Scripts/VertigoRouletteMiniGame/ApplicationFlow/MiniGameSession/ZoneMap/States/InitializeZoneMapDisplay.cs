using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    public class InitializeZoneMapDisplay : MiniGameSessionStateComponentBase
    {
        [SerializeField] private ZoneMapDisplayComponent zoneMapDisplayComponent;
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            yield return zoneMapDisplayComponent.InitializeZones(miniGameSessionComponent.GetZoneMapInstance());
            

        }
    }
}