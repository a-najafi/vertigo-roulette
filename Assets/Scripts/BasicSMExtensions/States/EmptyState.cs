using System.Collections;
using BasicSM;

namespace BasicSMExtensions.States
{
    public class EmptyState : StateComponentBase
    {
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return null;
        }

        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield return null;
        }

        public override IEnumerator OnUpdate(IStateMachine stateMachine)
        {
            yield return null;
        }

        public override IEnumerator OnFixedUpdate(IStateMachine stateMachine)
        {
            yield return null;
        }

        public override IEnumerator OnCleanUp(IStateMachine stateMachine)
        {
            yield return null;
        }
    }
}