using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BasicSMExtensions.States
{
    /// <summary>
    /// State that loads and runs a sub-state machine from an Addressable GameObject asset.
    /// </summary>
    public class RunSMState : StateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// The addressable reference to the sub-state machine prefab.
        /// </summary>
        [SerializeField]
        private AssetReference _smAsset;

        #endregion

        #region Non Serialized Parameters

        /// <summary>
        /// The instance of the sub-state machine that was spawned.
        /// </summary>
        private StateMachineComponentBase subStateMachineInstance = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads and instantiates the sub-state machine, and initializes it.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            var loadSMAsset = Addressables.LoadAssetAsync<GameObject>(_smAsset);
            yield return loadSMAsset;

            GameObject smInstance = Object.Instantiate(loadSMAsset.Result, transform, false);
            subStateMachineInstance = smInstance.GetComponent<StateMachineComponentBase>();

            yield return subStateMachineInstance.Initialize(stateMachine);
        }

        /// <summary>
        /// Delegates the OnUpdate loop to the sub-state machine instance.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnUpdate(IStateMachine stateMachine)
        {
            while (subStateMachineInstance == null)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return subStateMachineInstance.OnUpdate();
        }

        /// <summary>
        /// Delegates the OnFixedUpdate loop to the sub-state machine instance.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnFixedUpdate(IStateMachine stateMachine)
        {
            while (subStateMachineInstance == null)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return subStateMachineInstance.OnFixedUpdate();
        }

        /// <summary>
        /// Terminates and destroys the sub-state machine instance when exiting this state.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        /// <returns>Coroutine enumerator.</returns>
        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            if (subStateMachineInstance != null)
            {
                yield return subStateMachineInstance.Terminate();
                Object.Destroy(subStateMachineInstance.gameObject);
                subStateMachineInstance = null;
            }
        }

        #endregion
    }
}
