using BasicSM;
using UnityEngine;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine
{
    public class MiniGameStateMachineComponent : StateMachineComponentBase
    {
        [SerializeField]private MiniGameSessionComponent _miniGameSessionComponent;
        
        
        
        public MiniGameSessionComponent MiniGameSession => _miniGameSessionComponent;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            _miniGameSessionComponent = GetComponent<MiniGameSessionComponent>();
        }
#endif
        
        
    }
}