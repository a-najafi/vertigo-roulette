using System.Collections;
using UnityEngine;

namespace BasicSM
{
    public class StateComponentBase : MonoBehaviour, IState
    {
        public virtual IEnumerator OnEnter(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerator OnExit(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerator OnUpdate(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerator OnFixedUpdate(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerator OnCleanUp(IStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}