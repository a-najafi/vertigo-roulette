using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicSM
{
    /// <summary>
    /// Base class for defining state transitions in the state machine.
    /// </summary>
    [Serializable]
    public class StateTransitionBase : IStateTransition
    {
        #region Serialized Parameters

        [SerializeField]
        private GameObject targetStateGameObject;

        #endregion

        #region Non Serialized Parameters

        private StateComponentBase state;

        #endregion

        #region Properties

        /// <summary>
        /// The target state component for this transition.
        /// </summary>
        public IState TargetState => state ??= targetStateGameObject.GetComponent<StateComponentBase>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when this transition is initialized, e.g. when entering a state.
        /// </summary>
        /// <param name="stateMachine">The associated state machine.</param>
        /// <returns>Coroutine enumerator.</returns>
        public virtual IEnumerator Initialize(IStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when this transition is cleaned up, e.g. when exiting a state.
        /// </summary>
        /// <returns>Coroutine enumerator.</returns>
        public virtual IEnumerator CleanUp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether this transition should trigger, given the state machine's current state.
        /// </summary>
        /// <param name="stateMachine">The associated state machine.</param>
        /// <returns>True if the transition should occur; otherwise, false.</returns>
        public virtual bool ShouldTransition(IStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
