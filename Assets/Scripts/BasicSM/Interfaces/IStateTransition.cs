using System.Collections;
using System.Collections.Generic;

namespace BasicSM
{
    public interface IStateTransition
    {
        bool ShouldTransition(IStateMachine stateMachine);
        
        IState TargetState { get; }
    }
}