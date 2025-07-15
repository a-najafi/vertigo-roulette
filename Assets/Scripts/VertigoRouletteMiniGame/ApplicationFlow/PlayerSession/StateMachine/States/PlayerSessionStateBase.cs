using System;
using System.Collections;
using BasicSM;
using BasicSMExtensions.States;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.StateMachine.States
{
    /// <summary>
    /// Base class for all Player Session related states.
    /// Automatically fetches and assigns the PlayerSessionComponent from the associated state machine.
    /// </summary>
    public class PlayerSessionStateBase : EmptyState
    {
        #region Non-Serialized Parameters

        /// <summary>
        /// The PlayerSessionComponent attached to the state machine.
        /// This is set automatically during OnEnter.
        /// </summary>
        protected PlayerSessionComponent playerSessionComponent;

        #endregion

        #region State Logic

        /// <summary>
        /// Sets up the reference to the PlayerSessionComponent on enter.
        /// Throws if used with the wrong type of state machine.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            PlayerSessionStateMachine stateMachineComponent = stateMachine as PlayerSessionStateMachine;
            if (stateMachineComponent == null)
                throw new NullReferenceException("This state must be run by a state machine that has a PlayerSessionComponent.");

            playerSessionComponent = stateMachineComponent.PlayerSession;

            yield return null;
        }

        #endregion
    }
}