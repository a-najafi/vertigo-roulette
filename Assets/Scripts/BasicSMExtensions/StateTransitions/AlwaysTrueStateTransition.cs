using System;
using BasicSM;

namespace BasicSMExtensions
{
    [Serializable]
    public class AlwaysTrueStateTransition : StateTransitionBase
    {
        public override bool ShouldTransition(IStateMachine stateMachine)
        {
            return true;
        }
    }
}