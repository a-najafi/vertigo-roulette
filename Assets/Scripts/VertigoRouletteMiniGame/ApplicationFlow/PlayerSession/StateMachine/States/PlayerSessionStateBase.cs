using System;
using System.Collections;
using BasicSM;
using BasicSMExtensions.States;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.StateMachine.States
{
    public class PlayerSessionStateBase : EmptyState
    {
        protected PlayerSessionComponent playerSessionComponent;
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            PlayerSessionStateMachine stateMachineComponent = stateMachine as PlayerSessionStateMachine;
            if(stateMachineComponent == null)
                throw new NullReferenceException("This state must be run by a state machine that has a PlayerSessionComponent.");

            playerSessionComponent =
                stateMachineComponent.PlayerSession;

            yield return null;
        }
    }
}