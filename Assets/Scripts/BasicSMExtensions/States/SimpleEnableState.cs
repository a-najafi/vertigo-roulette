using System.Collections;
using System.Collections.Generic;
using BasicSM;
using UnityEngine;

namespace BasicSMExtensions.States
{
    /// <summary>
    /// A state that enables or disables a list of MonoBehaviour components when entered/exited.
    /// </summary>
    public class SimpleEnableState : EmptyState
    {
        #region Serialized Parameters

        /// <summary>
        /// The list of MonoBehaviour components to enable or disable.
        /// </summary>
        [SerializeField]
        private List<MonoBehaviour> _components;

        /// <summary>
        /// Whether to enable (true) or disable (false) the components on enter.
        /// </summary>
        [SerializeField]
        private bool _enable = true;

        #endregion

        #region Public Methods

        /// <summary>
        /// Enables or disables all specified components when entering the state.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            if (_components != null)
            {
                for (int i = 0; i < _components.Count; i++)
                {
                    _components[i].enabled = _enable;
                }
            }
        }

        /// <summary>
        /// Restores the previous enabled state to all specified components when exiting the state.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield return base.OnExit(stateMachine);
            if (_components != null)
            {
                for (int i = 0; i < _components.Count; i++)
                {
                    _components[i].enabled = !_enable;
                }
            }
        }

        #endregion
    }
}
