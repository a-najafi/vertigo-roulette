using System.Collections;
using BasicSM;
using BasicSMExtensions.States;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;

namespace VertigoRouletteMiniGame.ApplicationFlow.MainMenu.States
{
    /// <summary>
    /// State for clearing the player's inventory and saving an empty profile.
    /// </summary>
    public class ClearPlayerProfile : EmptyState
    {
        #region Public Methods

        /// <summary>
        /// On entering this state, clear the player's inventory and save the empty session.
        /// </summary>
        /// <param name="stateMachine">The state machine calling this state.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            PlayerSessionComponent playerSession = FindObjectOfType<PlayerSessionComponent>();
            playerSession.Session.Inventory.ClearInventory();
            yield return PlayerSession.PlayerSession.SavePlayerSession(playerSession.Session, SavePaths.PlayerSession);
            yield break;
        }

        /// <summary>
        /// On exiting this state, does nothing (no cleanup needed).
        /// </summary>
        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield break;
        }

        #endregion
    }
}