using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    /// <summary>
    /// State that advances the UI to display the next active zone in the zone map.
    /// </summary>
    public class MoveToNextActiveZoneDisplay : MiniGameSessionStateComponentBase
    {
        #region Serialized Parameters

        /// <summary>
        /// Reference to the component managing zone map UI displays.
        /// </summary>
        [SerializeField]
        private ZoneMapDisplayComponent _zoneMapDisplayComponent;

        #endregion

        #region State Logic

        /// <summary>
        /// Moves the UI to the next active zone using the current session's zone map instance.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            yield return _zoneMapDisplayComponent.MoveZonesNextByOne(miniGameSessionComponent.GetZoneMapInstance());
        }

        #endregion
    }
}