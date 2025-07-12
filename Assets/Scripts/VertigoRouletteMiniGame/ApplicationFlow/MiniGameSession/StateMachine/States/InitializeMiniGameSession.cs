using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States
{
    public class InitializeMiniGameSession : MiniGameSessionStateComponentBase
    {
        [SerializeField] private AssetLabelReference _configurationAssetLabel;
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            
            AsyncOperationHandle<MiniGameConfiguration> loadAssetAsync = Addressables.LoadAssetAsync<MiniGameConfiguration>(_configurationAssetLabel);
            yield return loadAssetAsync;

            if (loadAssetAsync.Result == null)
            {
                throw new System.Exception("Failed to load and initialize mini game config");
            }

            yield return miniGameSessionComponent.Initialize(loadAssetAsync.Result);

            

        }
    }
}