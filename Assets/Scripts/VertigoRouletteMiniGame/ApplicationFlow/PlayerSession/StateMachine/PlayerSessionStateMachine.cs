using BasicSM;
using UnityEngine;
using Utility.PropertyAttributes;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.StateMachine
{
    /// <summary>
    /// State machine root for player session operations.
    /// Holds reference to the PlayerSessionComponent on the same GameObject.
    /// </summary>
    public class PlayerSessionStateMachine : StateMachineComponentBase
    {
        #region Serialized Parameters

        [SerializeField, ReadOnly]
        private PlayerSessionComponent _playerSessionComponent;

        #endregion

        #region Properties

        /// <summary>
        /// The PlayerSessionComponent assigned at runtime or via OnValidate in editor.
        /// </summary>
        public PlayerSessionComponent PlayerSession => _playerSessionComponent;

        #endregion

#if UNITY_EDITOR

        #region Unity Editor Methods

        /// <summary>
        /// Automatically assign the PlayerSessionComponent reference in the Editor.
        /// </summary>
        private void OnValidate()
        {
            _playerSessionComponent = GetComponent<PlayerSessionComponent>();
        }

        #endregion

#endif
    }
}