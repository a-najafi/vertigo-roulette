using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicSM
{
    [Serializable]
    public class StateTransitionBase : IStateTransition
    {
        [SerializeField] private GameObject targetStateGameObject;
        private StateComponentBase state;

        public virtual IEnumerator Initialize(IStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerator CleanUp()
        {
            throw new NotImplementedException();
        }

        public virtual bool ShouldTransition(IStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        public IState TargetState => state ??= targetStateGameObject.GetComponent<StateComponentBase>();
    }
}