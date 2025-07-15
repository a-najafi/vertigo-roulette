using System.Collections;
using BasicSM;

namespace BasicSMExtensions.States
{
    /// <summary>
    /// A no-op (empty) state implementation for the state machine.
    /// All coroutines simply yield once and do nothing else.
    /// Useful as a placeholder or default state.
    /// </summary>
    public class EmptyState : StateComponentBase
    {
        #region Public Methods

        /// <summary>
        /// Called when entering this state. No-op.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return null;
        }

        /// <summary>
        /// Called when exiting this state. No-op.
        /// </summary>
        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield return null;
        }

        /// <summary>
        /// Called during update while this state is active. No-op.
        /// </summary>
        public override IEnumerator OnUpdate(IStateMachine stateMachine)
        {
            yield return null;
        }

        /// <summary>
        /// Called during fixed update while this state is active. No-op.
        /// </summary>
        public override IEnumerator OnFixedUpdate(IStateMachine stateMachine)
        {
            yield return null;
        }

        /// <summary>
        /// Called when cleaning up this state. No-op.
        /// </summary>
        public override IEnumerator OnCleanUp(IStateMachine stateMachine)
        {
            yield return null;
        }

        #endregion
    }
}