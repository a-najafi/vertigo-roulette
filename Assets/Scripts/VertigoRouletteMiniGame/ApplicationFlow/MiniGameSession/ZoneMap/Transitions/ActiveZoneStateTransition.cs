using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.Transitions
{
    public class ActiveZoneStateTransition : StateTransitionBase
    {
        [SerializeField]private EZoneState _requiredZoneState;
        private MiniGameSessionComponent miniGameSessionComponent;
        public override IEnumerator Initialize(IStateMachine stateMachine)
        {
            MiniGameStateMachineComponent stateMachineComponent = stateMachine as MiniGameStateMachineComponent;
            if(stateMachineComponent == null)
                throw new System.Exception("Invalid stateMachine. Expected MiniGameStateMachineComponent");
            miniGameSessionComponent = stateMachineComponent.MiniGameSession;
            yield break;
        }

        public override IEnumerator CleanUp()
        {
            miniGameSessionComponent = null;
            yield break;
        }

        public override bool ShouldTransition(IStateMachine stateMachine)
        {
            if(miniGameSessionComponent == null)
                throw new System.Exception("Invalid stateMachine or transition is not initialized");
            return miniGameSessionComponent.GetActiveZoneInstance().ZoneState == _requiredZoneState;
        }
    }
}