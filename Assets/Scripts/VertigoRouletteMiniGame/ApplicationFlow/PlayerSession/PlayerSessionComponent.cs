using UnityEngine;
using Utility.PropertyAttributes;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    /// <summary>
    /// MonoBehaviour wrapper for the runtime player session data.
    /// </summary>
    public class PlayerSessionComponent : MonoBehaviour
    {
        #region Non-Serialized Parameters

        [ReadOnly]
        private PlayerSession _playerSession;

        #endregion

        #region Properties

        /// <summary>
        /// The current runtime PlayerSession instance.
        /// </summary>
        public PlayerSession Session => _playerSession;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this component with a PlayerSession instance.
        /// </summary>
        /// <param name="playerSession">The PlayerSession data to use.</param>
        public void Initialize(PlayerSession playerSession)
        {
            _playerSession = playerSession;
        }

        #endregion
    }
}