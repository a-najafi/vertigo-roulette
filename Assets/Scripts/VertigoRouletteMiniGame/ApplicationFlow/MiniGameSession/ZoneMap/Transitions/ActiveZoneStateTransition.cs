using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.Transitions
{
    /// <summary>
    /// Transition that triggers when the active zone state matches the required value.
    /// </summary>
    public class ActiveZoneStateTransition : StateTransitionBase
    {
        #region Serialized Parameters

        /// <summary>
        /// The required zone state for this transition to be valid.
        /// </summary>
        [SerializeField]
        private EZoneState _requiredZoneState;

        #endregion

        #region Non-Serialized Parameters

        /// <summary>
        /// Cached reference to the MiniGameSessionComponent, set during Initialize.
        /// </summary>
        private MiniGameSessionComponent miniGameSessionComponent;

        #endregion

        #region Lifecycle Methods

        /// <summary>
        /// Initialize the transition and cache the MiniGameSessionComponent reference.
        /// </summary>
        public override IEnumerator Initialize(IStateMachine stateMachine)
        {
            MiniGameStateMachineComponent stateMachineComponent = stateMachine as MiniGameStateMachineComponent;
            if (stateMachineComponent == null)
                throw new System.Exception("Invalid stateMachine. Expected MiniGameStateMachineComponent");

            miniGameSessionComponent = stateMachineComponent.MiniGameSession;
            yield break;
        }

        /// <summary>
        /// Cleanup logic for the transition.
        /// </summary>
        public override IEnumerator CleanUp()
        {
            miniGameSessionComponent = null;
            yield break;
        }

        #endregion

        #region Transition Logic

        /// <summary>
        /// Checks if the transition should occur based on the active zone's state.
        /// </summary>
        public override bool ShouldTransition(IStateMachine stateMachine)
        {
            if (miniGameSessionComponent == null)
                throw new System.Exception("Invalid stateMachine or transition is not initialized");

            return miniGameSessionComponent.GetActiveZoneInstance().ZoneState == _requiredZoneState;
        }

        #endregion
    }
}
