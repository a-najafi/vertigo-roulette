using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Utility.BasicFSMExtensions
{
    public class LoadSceneState: StateComponentBase
    {
        [SerializeField]private AssetReference sceneAsset;

        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            var loadSceneAssetAsync = sceneAsset.LoadSceneAsync();
            yield return loadSceneAssetAsync;
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
    }
}