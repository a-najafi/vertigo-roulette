using System;
using System.Collections;
using BasicSM;
using BasicSMExtensions.States;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States
{
    public class MiniGameSessionStateComponentBase : EmptyState
    {
        protected MiniGameSessionComponent miniGameSessionComponent;

        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            MiniGameStateMachineComponent stateMachineComponent = stateMachine as MiniGameStateMachineComponent;
            if (stateMachineComponent == null)
                throw new NullReferenceException(
                    "This state must be run by a state machine that has a MiniGameSessionComponent.");

            miniGameSessionComponent =
                stateMachineComponent.MiniGameSession;

            yield return null;
        }

    }
}