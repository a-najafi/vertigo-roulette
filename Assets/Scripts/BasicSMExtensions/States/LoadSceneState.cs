using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace BasicSMExtensions.States
{
    /// <summary>
    /// State that loads a scene via an Addressable AssetReference when entered.
    /// </summary>
    public class LoadSceneState : StateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// The addressable reference to the scene asset to load.
        /// </summary>
        [SerializeField]
        private AssetReference sceneAsset;

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the specified scene asynchronously when entering this state.
        /// </summary>
        /// <param name="stateMachine">The associated state machine.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            var loadSceneAssetAsync = sceneAsset.LoadSceneAsync(LoadSceneMode.Single, true);
            yield return loadSceneAssetAsync;
        }

        /// <summary>
        /// Called when exiting this state. No operation by default.
        /// </summary>
        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield return null;
        }

        /// <summary>
        /// Called during the update loop for this state. No operation by default.
        /// </summary>
        public override IEnumerator OnUpdate(IStateMachine stateMachine)
        {
            yield return null;
        }

        /// <summary>
        /// Called during the fixed update loop for this state. No operation by default.
        /// </summary>
        public override IEnumerator OnFixedUpdate(IStateMachine stateMachine)
        {
            yield return null;
        }

        #endregion
    }
}