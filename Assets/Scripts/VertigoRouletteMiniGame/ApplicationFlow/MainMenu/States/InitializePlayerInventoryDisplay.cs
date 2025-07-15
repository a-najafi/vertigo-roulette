using System.Collections;
using BasicSM;
using BasicSMExtensions.States;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;

namespace VertigoRouletteMiniGame.ApplicationFlow.MainMenu.States
{
    /// <summary>
    /// State for initializing and showing the player inventory display in the main menu.
    /// </summary>
    public class InitializePlayerInventoryDisplay : EmptyState
    {
        #region Serialized Parameters

        /// <summary>
        /// The inventory UI display to initialize and show/hide.
        /// </summary>
        [SerializeField] private PlayerInventoryDisplay _inventoryDisplay;

        #endregion

        #region Non Serialized Parameters

        private PlayerSessionComponent playerSession;

        #endregion

        #region Public Methods

        /// <summary>
        /// On entering this state, initializes the inventory display and shows it.
        /// </summary>
        /// <param name="stateMachine">The state machine invoking this state.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            if (playerSession == null)
            {
                playerSession = FindObjectOfType<PlayerSessionComponent>();
            }

            yield return _inventoryDisplay.Initialize(playerSession.Session.Inventory);
            _inventoryDisplay.gameObject.SetActive(true);
        }

        /// <summary>
        /// On exiting this state, hides the inventory display.
        /// </summary>
        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            _inventoryDisplay.gameObject.SetActive(false);
            yield break;
        }

        #endregion
    }
}