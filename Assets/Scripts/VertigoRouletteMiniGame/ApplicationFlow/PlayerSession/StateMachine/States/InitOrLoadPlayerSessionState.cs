using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utility.Addressable;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.StateMachine.States
{
    public class InitOrLoadPlayerSessionState : PlayerSessionStateBase
    {
        [SerializeField]private AssetLabelReference _activePlayerSessionStartingConfigLabel;
        



        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            AssetLoadResult<PlayerSession> loadedPlayerSession = new AssetLoadResult<PlayerSession>();
            yield return PlayerSession.LoadPlayerSession(loadedPlayerSession, SavePaths.PlayerSession);
            if(loadedPlayerSession.Asset != null)
                playerSessionComponent.Initialize(loadedPlayerSession.Asset);
            else
                yield return InitializePlayerSession();
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

      

        public IEnumerator InitializePlayerSession()
        {
            if(!_activePlayerSessionStartingConfigLabel.RuntimeKeyIsValid())
                throw new Exception("Invalid active player session configuration label");
            
            PlayerSession playerSession =  new PlayerSession();
            AsyncOperationHandle<PlayerSessionStartingConfig> loadAssetAsync = Addressables.LoadAssetAsync<PlayerSessionStartingConfig>(_activePlayerSessionStartingConfigLabel);
            yield return loadAssetAsync;

            if (loadAssetAsync.Result == null)
            {
                throw new System.Exception("Failed to load player session starting config");
            }

            PlayerSessionStartingConfig playerSessionStartingConfig = loadAssetAsync.Result;
            int startingInventoryCount = playerSessionStartingConfig.PlayerInventoryItemDesigns.Count;
            for (int i = 0; i < startingInventoryCount; i++)
            {
                playerSession.Inventory.IncreaseCount(playerSessionStartingConfig.PlayerInventoryItemDesigns[i].ItemDefinitionAssetReference.AssetGUID,playerSessionStartingConfig.PlayerInventoryItemDesigns[i].Count);
            }
            yield return PlayerSession.SavePlayerSession(playerSession, SavePaths.PlayerSession);

        }

      
    }
}