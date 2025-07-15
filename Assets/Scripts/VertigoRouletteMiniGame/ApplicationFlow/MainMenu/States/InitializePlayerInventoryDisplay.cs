using System.Collections;
using BasicSM;
using BasicSMExtensions.States;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;

namespace VertigoRouletteMiniGame.ApplicationFlow.MainMenu.States
{
    public class InitializePlayerInventoryDisplay : EmptyState
    {
        
        [SerializeField] private PlayerInventoryDisplay _inventoryDisplay;
        private PlayerSessionComponent playerSession;


        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            
            if (playerSession == null)
            {
                playerSession = FindObjectOfType<PlayerSessionComponent>();
            }

            yield return _inventoryDisplay.Initialize(playerSession.Session.Inventory);
            _inventoryDisplay.gameObject.SetActive(true);
        }

        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            
            _inventoryDisplay.gameObject.SetActive(false);
            yield break;
        }
    }
}