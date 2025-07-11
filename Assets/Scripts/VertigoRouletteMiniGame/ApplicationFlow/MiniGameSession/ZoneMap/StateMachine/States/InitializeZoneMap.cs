using System.Collections;
using BasicSM;
using BasicSMExtensions.States;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.StateMachine.States
{
    public class InitializeZoneMap : EmptyState
    {
        
        [SerializeField]private AssetLabelReference _activeZoneMapConfigurationLabel;
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            AsyncOperationHandle<ZoneMapConfigurationBase> loadAssetAsync = Addressables.LoadAssetAsync<ZoneMapConfigurationBase>(_activeZoneMapConfigurationLabel);
            yield return loadAssetAsync;

            if (loadAssetAsync.Result == null)
            {
                throw new System.Exception("Failed to load player session starting config");
            }

            ZoneMapConfigurationBase zoneMapConfiguration = loadAssetAsync.Result;
            
        }
    }
}