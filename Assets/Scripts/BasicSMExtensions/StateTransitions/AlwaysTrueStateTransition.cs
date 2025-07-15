using System;
using System.Collections;
using BasicSM;

namespace BasicSMExtensions.StateTransitions
{
    /// <summary>
    /// A state transition that always returns true for ShouldTransition,
    /// causing the transition to always be triggered when evaluated.
    /// </summary>
    [Serializable]
    public class AlwaysTrueStateTransition : StateTransitionBase
    {
        #region Public Methods

        /// <summary>
        /// Called when this transition is initialized. No additional logic.
        /// </summary>
        public override IEnumerator Initialize(IStateMachine stateMachine)
        {
            yield return null;
        }

        /// <summary>
        /// Called when this transition is cleaned up. No additional logic.
        /// </summary>
        public override IEnumerator CleanUp()
        {
            yield return null;
        }

        /// <summary>
        /// Always returns true, indicating that the transition should always occur.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        /// <returns>True, always.</returns>
        public override bool ShouldTransition(IStateMachine stateMachine)
        {
            return true;
        }

        #endregion
    }
}