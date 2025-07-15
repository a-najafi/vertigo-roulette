using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States
{
    /// <summary>
    /// State responsible for loading and initializing the minigame configuration.
    /// </summary>
    public class InitializeMiniGameSession : MiniGameSessionStateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// Asset label reference for the MiniGameConfiguration ScriptableObject.
        /// </summary>
        [SerializeField]
        private AssetLabelReference _configurationAssetLabel;

        #endregion

        #region State Logic

        /// <summary>
        /// Loads the minigame configuration using Addressables and initializes the miniGameSessionComponent.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            AsyncOperationHandle<MiniGameConfiguration> loadAssetAsync =
                Addressables.LoadAssetAsync<MiniGameConfiguration>(_configurationAssetLabel);
            yield return loadAssetAsync;

            if (loadAssetAsync.Result == null)
            {
                throw new System.Exception("Failed to load and initialize mini game config");
            }

            yield return miniGameSessionComponent.Initialize(loadAssetAsync.Result);
        }

        #endregion
    }
}