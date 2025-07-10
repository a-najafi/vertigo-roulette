using System;
using System.Collections;
using BasicSM;

namespace BasicSMExtensions
{
    [Serializable]
    public class AlwaysTrueStateTransition : StateTransitionBase
    {
        public override IEnumerator Initialize(IStateMachine stateMachine)
        {
            yield return null;
        }

        public override IEnumerator CleanUp()
        {
            yield return null;
        }

        public override bool ShouldTransition(IStateMachine stateMachine)
        {
            return true;
        }
    }
}