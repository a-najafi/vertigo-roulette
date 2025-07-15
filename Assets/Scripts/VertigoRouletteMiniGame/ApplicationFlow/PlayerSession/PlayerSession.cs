using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    [Serializable]
    public class PlayerSession : IPlayerSession
    {
        [SerializeField,ReadOnly]private string name = "DefaultPlayerSession";
        [SerializeField,ReadOnly]private PlayerInventory inventory = new PlayerInventory();
        
        public string Name => name;
        public PlayerInventory Inventory => inventory;
        
        public static IEnumerator SavePlayerSession(PlayerSession playerSession, string savePath)
        {
            string json = JsonUtility.ToJson(playerSession, true);
            Task writeAsyncOperationHandle= File.WriteAllTextAsync(savePath, json);
            yield return writeAsyncOperationHandle;
            
        }
        
        public static IEnumerator LoadPlayerSession(AssetLoadResult<PlayerSession> loadedPlayerSession, string savePath)
        {
            var readAllTextAsync = File.ReadAllTextAsync(savePath);
            yield return readAllTextAsync;
            if (readAllTextAsync.IsCompletedSuccessfully)
            {
                PlayerSession playerSession =  JsonUtility.FromJson<PlayerSession>(readAllTextAsync.Result);
                if(playerSession != null)
                    loadedPlayerSession.Asset = playerSession;    
            }
            
        }
    }
}