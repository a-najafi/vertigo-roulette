using System.Collections;
using System.Collections.Generic;

namespace BasicSM
{
    public interface IStateTransition
    {
        
        IEnumerator Initialize(IStateMachine stateMachine);
        
        IEnumerator CleanUp();
        
        bool ShouldTransition(IStateMachine stateMachine);
        
        IState TargetState { get; }
    }
}