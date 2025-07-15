using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States
{
    public class InitializeRewardInventoryDisplay : MiniGameSessionStateComponentBase
    {
        [SerializeField] private PlayerInventoryDisplay _inventoryDisplay;


        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            yield return _inventoryDisplay.Initialize(miniGameSessionComponent.ObtainedRewardInventory);
            _inventoryDisplay.gameObject.SetActive(true);
        }

        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            
            _inventoryDisplay.gameObject.SetActive(false);
            yield break;
        }
    }
}