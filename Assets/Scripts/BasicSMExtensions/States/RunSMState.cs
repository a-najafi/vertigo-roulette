using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utility.BasicFSMExtensions
{
    public class RunSMState : StateComponentBase
    {
        [SerializeField]private AssetReference _smAsset;
        private StateMachineComponentBase subStateMachineInstance = null;

        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            var loadSMAsset = Addressables.LoadAssetAsync<GameObject>(_smAsset);
            yield return loadSMAsset;

            GameObject smInstance = Instantiate(loadSMAsset.Result, transform,false);
            StateMachineComponentBase stateMachineComponentBase = smInstance.GetComponent<StateMachineComponentBase>();
            
            yield return stateMachineComponentBase.Initialize(stateMachine);
        }

        public override IEnumerator OnUpdate(IStateMachine stateMachine)
        {
            while (subStateMachineInstance == null)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return subStateMachineInstance.OnUpdate();
            
        }

        public override IEnumerator OnFixedUpdate(IStateMachine stateMachine)
        {
            while (subStateMachineInstance == null)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return subStateMachineInstance.OnFixedUpdate();
        }

        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield return subStateMachineInstance.Terminate();
            Destroy(subStateMachineInstance.gameObject);
            subStateMachineInstance = null;
        }
    }
}