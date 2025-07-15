using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States
{
    /// <summary>
    /// State for initializing and displaying the reward inventory in the minigame session.
    /// </summary>
    public class InitializeRewardInventoryDisplay : MiniGameSessionStateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// Reference to the PlayerInventoryDisplay component for showing obtained rewards.
        /// </summary>
        [SerializeField]
        private PlayerInventoryDisplay _inventoryDisplay;

        #endregion

        #region State Logic

        /// <summary>
        /// Shows the reward inventory and initializes it with the obtained rewards when entering this state.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            yield return _inventoryDisplay.Initialize(miniGameSessionComponent.ObtainedRewardInventory);
            _inventoryDisplay.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the reward inventory display when exiting this state.
        /// </summary>
        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            _inventoryDisplay.gameObject.SetActive(false);
            yield break;
        }

        #endregion
    }
}