
using System.Collections;

namespace BasicSM
{
    public interface IState
    {
        IEnumerator OnEnter(IStateMachine stateMachine);
        
        IEnumerator OnExit(IStateMachine stateMachine);
        
        IEnumerator OnUpdate(IStateMachine stateMachine);
        
        IEnumerator OnFixedUpdate(IStateMachine stateMachine);

        IEnumerator OnCleanUp(IStateMachine stateMachine);
    }
}