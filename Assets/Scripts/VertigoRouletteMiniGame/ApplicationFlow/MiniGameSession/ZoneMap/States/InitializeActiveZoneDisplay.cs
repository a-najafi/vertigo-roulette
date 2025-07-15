using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    /// <summary>
    /// State that initializes the roulette for the currently active zone display.
    /// </summary>
    public class InitializeActiveZoneDisplay : MiniGameSessionStateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// Reference to the ZoneMapDisplayComponent that manages zone UIs.
        /// </summary>
        [SerializeField]
        private ZoneMapDisplayComponent zoneMapDisplayComponent;

        #endregion

        #region State Logic

        /// <summary>
        /// Calls InitializeRoulette on the active zone display during state entry.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            yield return zoneMapDisplayComponent.GetActiveZoneDisplay().InitializeRoulette();
        }

        #endregion
    }
}