using System;
using System.Collections.Generic;
using UnityEngine;

namespace BasicSM
{
    [Serializable]
    public class StateConfigurationBase : IStateConfiguration
    {
        [SerializeField] StateComponentBase _state;
        [SerializeReference, SerializeField]private List<StateTransitionBase>  _serializedTransitions;

        private List<IStateTransition>  _transitions = null;
        
        public IState State => _state;
        
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