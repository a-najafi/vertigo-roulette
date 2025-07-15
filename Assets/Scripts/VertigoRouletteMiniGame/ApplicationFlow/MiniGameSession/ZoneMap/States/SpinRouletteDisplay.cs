using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    /// <summary>
    /// State that visually spins the roulette wheel in the active zone display and then reveals the final reward.
    /// </summary>
    public class SpinRouletteDisplay : MiniGameSessionStateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// Reference to the component managing zone map displays.
        /// </summary>
        [SerializeField] 
        private ZoneMapDisplayComponent _zoneMapDisplayComponent;

        #endregion

        #region State Logic

        /// <summary>
        /// Spins the roulette UI in the active zone display and displays the reward.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            // Get the currently active zone display UI
            ZoneDisplayComponent activeZoneDisplayComponent = _zoneMapDisplayComponent.GetActiveZoneDisplay();

            // Spin the roulette visually and wait for the animation to complete
            yield return activeZoneDisplayComponent
                .SpinRoulette(activeZoneDisplayComponent.ZoneInstance.RouletteInstance.ResultIndex);

            // Display the final reward after spinning
            yield return activeZoneDisplayComponent.DisplayFinalReward();
        }

        #endregion
    }
}