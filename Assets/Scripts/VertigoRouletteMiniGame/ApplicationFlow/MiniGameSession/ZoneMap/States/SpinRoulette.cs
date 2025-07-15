using System.Collections;
using BasicSM;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.StateMachine.States;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.States
{
    /// <summary>
    /// State responsible for spinning the roulette in the current zone,
    /// awarding the player if a reward is won.
    /// </summary>
    public class SpinRoulette : MiniGameSessionStateComponentBase
    {
        #region State Logic

        /// <summary>
        /// Spins the roulette, handles reward distribution if the player wins.
        /// </summary>
        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);

            ZoneInstance activeZone = miniGameSessionComponent.GetActiveZoneInstance();

            // Spin the roulette (may take several frames)
            yield return miniGameSessionComponent.SpinRoulette();

            // If the outcome is not a win, exit early
            if (activeZone.ZoneState != EZoneState.Win)
                yield break;

            // Retrieve and log the winning reward
            RouletteRewardConfiguration rewardConfiguration = activeZone.RouletteInstance.ResultRewardConfiguration;
            Debug.Log("Reward obtained at index " + activeZone.RouletteInstance.ResultIndex +
                      ": item " + rewardConfiguration.ItemDefinition.AssetGUID +
                      " (count: " + rewardConfiguration.Amount + ")");

            // Add reward to player session
            miniGameSessionComponent.AddReward(rewardConfiguration);
        }

        #endregion
    }
}