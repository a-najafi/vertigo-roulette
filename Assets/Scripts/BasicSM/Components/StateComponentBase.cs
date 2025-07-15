using System.Collections;
using UnityEngine;

namespace BasicSM
{
    /// <summary>
    /// Base MonoBehaviour for implementing a state in a state machine.
    /// Provides virtual coroutine methods for entering, exiting, updating, and cleaning up state logic.
    /// </summary>
    public class StateComponentBase : MonoBehaviour, IState
    {
        #region Public Methods

        /// <summary>
        /// Called when the state machine enters this state.
        /// Override to implement custom enter logic.
        /// </summary>
        /// <param name="stateMachine">The state machine instance.</param>
        /// <returns>Coroutine enumerator.</returns>
        public virtual IEnumerator OnEnter(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Called when the state machine exits this state.
        /// Override to implement custom exit logic.
        /// </summary>
        /// <param name="stateMachine">The state machine instance.</param>
        /// <returns>Coroutine enumerator.</returns>
        public virtual IEnumerator OnExit(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Called during the state machine's update loop while this state is active.
        /// Override to implement per-frame update logic.
        /// </summary>
        /// <param name="stateMachine">The state machine instance.</param>
        /// <returns>Coroutine enumerator.</returns>
        public virtual IEnumerator OnUpdate(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Called during the state machine's fixed update loop while this state is active.
        /// Override to implement physics or fixed-interval logic.
        /// </summary>
        /// <param name="stateMachine">The state machine instance.</param>
        /// <returns>Coroutine enumerator.</returns>
        public virtual IEnumerator OnFixedUpdate(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Called when the state is being cleaned up (e.g., state machine shutdown).
        /// Override to implement custom cleanup logic.
        /// </summary>
        /// <param name="stateMachine">The state machine instance.</param>
        /// <returns>Coroutine enumerator.</returns>
        public virtual IEnumerator OnCleanUp(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
