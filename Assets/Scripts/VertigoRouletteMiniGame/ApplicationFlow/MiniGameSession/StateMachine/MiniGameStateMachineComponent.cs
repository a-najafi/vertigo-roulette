using BasicSM;
using UnityEngine;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine
{
    /// <summary>
    /// State machine component specifically for minigame sessions,
    /// provides access to the associated MiniGameSessionComponent.
    /// </summary>
    public class MiniGameStateMachineComponent : StateMachineComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// The MiniGameSessionComponent managed by this state machine.
        /// </summary>
        [SerializeField]
        private MiniGameSessionComponent _miniGameSessionComponent;

        #endregion

        #region Properties

        /// <summary>
        /// Public accessor for the session component.
        /// </summary>
        public MiniGameSessionComponent MiniGameSession => _miniGameSessionComponent;

        #endregion

        #region Editor Utilities

#if UNITY_EDITOR
        /// <summary>
        /// Auto-assign the session component in the editor for convenience.
        /// </summary>
        private void OnValidate()
        {
            _miniGameSessionComponent = GetComponent<MiniGameSessionComponent>();
        }
#endif

        #endregion
    }
}