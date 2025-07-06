using System.Collections.Generic;

namespace BasicSM
{
    public interface IStateConfiguration
    {
        IState State{ get;}
        List<IStateTransition> Transitions{ get; }
    }
}