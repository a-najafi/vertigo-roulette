using System;
using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States
{
    /// <summary>
    /// State for leaving the minigame, saving rewards, and loading the main menu scene.
    /// </summary>
    public class LeaveGameAndTakeRewards : MiniGameSessionStateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// Addressable reference for the main menu scene to load after leaving.
        /// </summary>
        [SerializeField]
        private AssetReference _mainMenuScene; 

        #endregion

        #region State Logic

        /// <summary>
        /// Saves rewards (if not lost) and loads the main menu scene.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            // Only award rewards if the player did not lose the current zone
            if (miniGameSessionComponent.GetActiveZoneInstance().ZoneState != EZoneState.Lose)
            {
                PlayerSessionComponent playerSessionComponent = FindObjectOfType<PlayerSessionComponent>();
                if (playerSessionComponent != null)
                {
                    // Transfer obtained rewards to player inventory and save
                    playerSessionComponent.Session.Inventory.IncreaseCountByInventory(
                        miniGameSessionComponent.ObtainedRewardInventory
                    );
                    yield return PlayerSession.PlayerSession.SavePlayerSession(
                        playerSessionComponent.Session,
                        SavePaths.PlayerSession
                    );
                }
                else
                {
                    throw new Exception("PlayerSessionComponent is null");
                }
            }

            // Load main menu scene via Addressables
            var loadSceneAssetAsync = _mainMenuScene.LoadSceneAsync(LoadSceneMode.Single, true);
            yield return loadSceneAssetAsync;
        }

        #endregion
    }
}
