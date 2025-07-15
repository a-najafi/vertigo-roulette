using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    /// <summary>
    /// State that initializes the zone map display with the current zone map instance.
    /// </summary>
    public class InitializeZoneMapDisplay : MiniGameSessionStateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// Reference to the ZoneMapDisplayComponent for visualizing zones.
        /// </summary>
        [SerializeField]
        private ZoneMapDisplayComponent zoneMapDisplayComponent;

        #endregion

        #region State Logic

        /// <summary>
        /// Initializes the zone map display using the session's zone map instance.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            yield return zoneMapDisplayComponent.InitializeZones(miniGameSessionComponent.GetZoneMapInstance());
        }

        #endregion
    }
}