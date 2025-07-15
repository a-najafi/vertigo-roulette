using System;
using System.Collections;
using BasicSM;
using BasicSMExtensions.States;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States
{
    /// <summary>
    /// Base state for all mini-game session states, providing easy access to the MiniGameSessionComponent.
    /// </summary>
    public class MiniGameSessionStateComponentBase : EmptyState
    {
        #region Protected Parameters

        /// <summary>
        /// Reference to the MiniGameSessionComponent (set during OnEnter).
        /// </summary>
        protected MiniGameSessionComponent miniGameSessionComponent;

        #endregion

        #region State Logic

        /// <summary>
        /// Assigns the MiniGameSessionComponent from the state machine on entry.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            MiniGameStateMachineComponent stateMachineComponent = stateMachine as MiniGameStateMachineComponent;
            if (stateMachineComponent == null)
                throw new NullReferenceException(
                    "This state must be run by a state machine that has a MiniGameSessionComponent.");

            miniGameSessionComponent = stateMachineComponent.MiniGameSession;

            yield return null;
        }

        #endregion
    }
}