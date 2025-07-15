using System;
using System.Collections.Generic;
using UnityEngine;

namespace BasicSM
{
    /// <summary>
    /// Serializable configuration for a single state, including its transitions and the associated state GameObject.
    /// </summary>
    [Serializable]
    public class StateConfigurationBase : IStateConfiguration
    {
        #region Serialized Parameters

        [SerializeField]
        private GameObject _stateGameObject;

        [SerializeField, SerializeReference]
        private List<StateTransitionBase> _serializedTransitions;

        #endregion

        #region Non Serialized Parameters

        private StateComponentBase state;
        private List<IStateTransition> _transitions = null;

        #endregion

        #region Properties

        /// <summary>
        /// The state component associated with this configuration.
        /// </summary>
        public IState State => state ??= _stateGameObject.GetComponent<StateComponentBase>();

        /// <summary>
        /// List of state transitions associated with this state.
        /// </summary>
        public List<IStateTransition> Transitions
        {
            get
            {
                if (_transitions == null)
                {
                    _transitions = new List<IStateTransition>();
                    int transitionCount = _serializedTransitions.Count;
                    for (int i = 0; i < transitionCount; i++)
                    {
                        _transitions.Add(_serializedTransitions[i]);
                    }
                }
                return _transitions;
            }
        }

        #endregion
    }
}