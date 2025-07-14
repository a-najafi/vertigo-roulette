using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.StateMachine.States
{
    public class InitOrLoadPlayerSessionState : PlayerSessionStateBase
    {
        [SerializeField]private AssetLabelReference _activePlayerSessionStartingConfigLabel;
        

        private static string SavePath => Path.Combine(Application.persistentDataPath, "playerSession_save.json");

        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            
            yield return LoadPlayerSession();

            if (playerSessionComponent.Session == null)
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

        public IEnumerator LoadPlayerSession()
        {
            var readAllTextAsync = File.ReadAllTextAsync(SavePath);
            yield return readAllTextAsync;
            if (readAllTextAsync.IsCompletedSuccessfully)
            {
                PlayerSession playerSession =  JsonUtility.FromJson<PlayerSession>(readAllTextAsync.Result);
                if(playerSession != null)
                    playerSessionComponent.Initialize(playerSession);    
            }
            
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

            yield return SavePlayerSession(playerSession);

        }

        public IEnumerator SavePlayerSession(PlayerSession playerSession)
        {
            string json = JsonUtility.ToJson(playerSession, true);
            Task writeAsyncOperationHandle= File.WriteAllTextAsync(SavePath, json);
            yield return writeAsyncOperationHandle;
            
        }
    }
}