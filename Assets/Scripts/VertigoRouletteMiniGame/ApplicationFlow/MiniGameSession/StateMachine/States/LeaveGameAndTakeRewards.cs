using System;
using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;


namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States
{
    public class LeaveGameAndTakeRewards : MiniGameSessionStateComponentBase
    {
        [SerializeField] private AssetReference _mainMenuScene; 
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return  base.OnEnter(stateMachine);
            if (miniGameSessionComponent.GetActiveZoneInstance().ZoneState != EZoneState.Lose)
            {
                PlayerSessionComponent playerSessionComponent = FindObjectOfType<PlayerSessionComponent>();
                if (playerSessionComponent != null)
                {
                    playerSessionComponent.Session.Inventory.IncreaseCountByInventory(miniGameSessionComponent
                        .ObtainedRewardInventory);
                    yield return PlayerSession.PlayerSession.SavePlayerSession(playerSessionComponent.Session,
                        SavePaths.PlayerSession);
                }
                else
                {
                    throw new Exception("PlayerSessionComponent is null");
                }
            }
            
            var loadSceneAssetAsync = _mainMenuScene.LoadSceneAsync(LoadSceneMode.Single,true);
            yield return loadSceneAssetAsync;
            
            
            
            
            
        }
    }
}