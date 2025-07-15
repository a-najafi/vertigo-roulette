using System.Collections;
using System.Collections.Generic;

namespace BasicSM
{
    
    
    
    public interface IStateMachine
    {
        List<IStateConfiguration> States { get; }
        
        IStateConfiguration CurrentState { get; }
        
        
        IEnumerator Initialize(IStateMachine stateMachine = null);
        
        IEnumerator OnUpdate();
        
        IEnumerator Terminate();
        
    }
}