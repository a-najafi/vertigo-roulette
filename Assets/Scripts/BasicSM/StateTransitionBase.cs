using System;
using System.Collections.Generic;
using UnityEngine;

namespace BasicSM
{
    [Serializable]
    public class StateTransitionBase : IStateTransition
    {
        [SerializeField] private StateComponentBase _state;
        
        public virtual bool ShouldTransition(IStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        public IState TargetState => _state;
    }
}