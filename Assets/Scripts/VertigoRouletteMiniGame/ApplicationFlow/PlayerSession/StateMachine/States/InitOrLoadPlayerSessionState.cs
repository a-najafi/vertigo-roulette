using System;
using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utility.Addressable;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.StateMachine.States
{
    /// <summary>
    /// State responsible for initializing or loading a player's session,
    /// including starting inventory when no previous session exists.
    /// </summary>
    public class InitOrLoadPlayerSessionState : PlayerSessionStateBase
    {
        #region Serialized Parameters

        [SerializeField]
        private AssetLabelReference _activePlayerSessionStartingConfigLabel;

        #endregion

        #region Public State Logic

        /// <summary>
        /// On state enter, loads player session from disk if available, or initializes a new one.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            // Try to load existing player session from disk
            AssetLoadResult<PlayerSession> loadedPlayerSession = new AssetLoadResult<PlayerSession>();
            yield return PlayerSession.LoadPlayerSession(loadedPlayerSession, SavePaths.PlayerSession);

            if (loadedPlayerSession.Asset != null)
            {
                // If session exists, initialize player session component with it
                playerSessionComponent.Initialize(loadedPlayerSession.Asset);
            }
            else
            {
                // Otherwise, initialize a brand new session
                yield return InitializePlayerSession();
            }
        }

        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield return null;
        }

        public override IEnumerator OnUpdate(IStateMachine stateMachine)
        {
            yield return null;
        }

        public override IEnumerator OnFixedUpdate(IStateMachine stateMachine)
        {
            yield return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes a new player session with starting inventory from a config asset.
        /// </summary>
        private IEnumerator InitializePlayerSession()
        {
            if (!_activePlayerSessionStartingConfigLabel.RuntimeKeyIsValid())
                throw new Exception("Invalid active player session configuration label");

            // Create empty player session object
            PlayerSession playerSession = new PlayerSession();

            // Load starting config asset for the session
            AsyncOperationHandle<PlayerSessionStartingConfig> loadAssetAsync =
                Addressables.LoadAssetAsync<PlayerSessionStartingConfig>(_activePlayerSessionStartingConfigLabel);
            yield return loadAssetAsync;

            if (loadAssetAsync.Result == null)
                throw new Exception("Failed to load player session starting config");

            PlayerSessionStartingConfig playerSessionStartingConfig = loadAssetAsync.Result;

            // Initialize inventory according to starting config
            int startingInventoryCount = playerSessionStartingConfig.PlayerInventoryItemDesigns.Count;
            for (int i = 0; i < startingInventoryCount; i++)
            {
                playerSession.Inventory.IncreaseCount(
                    playerSessionStartingConfig.PlayerInventoryItemDesigns[i].ItemDefinitionAssetReference.AssetGUID,
                    playerSessionStartingConfig.PlayerInventoryItemDesigns[i].Count);
            }

            // Persist to disk and initialize the main session component
            yield return PlayerSession.SavePlayerSession(playerSession, SavePaths.PlayerSession);
            playerSessionComponent.Initialize(playerSession);
        }

        #endregion
    }
}
