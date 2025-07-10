using System;
using System.Collections.Generic;
using UnityEngine;

namespace BasicSM
{
    [Serializable]
    public class StateConfigurationBase : IStateConfiguration
    {
        [SerializeField]private GameObject _stateGameObject; 
        private StateComponentBase state;
        [SerializeField, SerializeReference]private List<StateTransitionBase>  _serializedTransitions;

        private List<IStateTransition>  _transitions = null;

        public IState State => state ??= _stateGameObject.GetComponent<StateComponentBase>();
        
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
    }
}