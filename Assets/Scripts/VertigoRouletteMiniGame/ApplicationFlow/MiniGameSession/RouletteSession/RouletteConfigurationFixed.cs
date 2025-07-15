using System;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession
{
    /// <summary>
    /// ScriptableObject roulette configuration with a fixed, designer-defined set of rewards.
    /// </summary>
    [CreateAssetMenu(fileName = "RouletteConfigurationFixed", menuName = "MiniGame/RouletteConfigurationFixed")]
    public class RouletteConfigurationFixed : RouletteConfigurationBase
    {
        #region Serialized Parameters

        /// <summary>
        /// List of all reward/bomb configurations for this roulette wheel.
        /// </summary>
        [SerializeField]
        private List<RouletteRewardConfiguration> _rouletteRewardConfigurations = new List<RouletteRewardConfiguration>();

        #endregion

        #region Editor-Only: Probability Debug

#if UNITY_EDITOR
        /// <summary>
        /// Calculates and updates occurrence chance for each reward based on probability modifiers (for editor display).
        /// </summary>
        private void OnValidate()
        {
            int rewardCount = _rouletteRewardConfigurations.Count;
            float totalRewardWeight = 0;
            float[] weightCaps = new float[rewardCount];

            // Sum up weights
            for (int i = 0; i < rewardCount; i++)
            {
                weightCaps[i] += 1 + _rouletteRewardConfigurations[i].ProbabilityModifier;
                totalRewardWeight += weightCaps[i];
            }

            // Calculate and store chance %
            for (int i = 0; i < rewardCount; i++)
            {
                _rouletteRewardConfigurations[i].occurenceChance = weightCaps[i] / totalRewardWeight * 100;
            }
        }
#endif

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the configured reward list, optionally truncated to a max amount.
        /// </summary>
        /// <param name="maxAmount">If non-negative, returns at most this many rewards.</param>
        /// <returns>List of reward configurations for this roulette.</returns>
        public override List<RouletteRewardConfiguration> GetRewardConfigurations(int maxAmount = -1)
        {
            if (maxAmount < 0)
                return _rouletteRewardConfigurations;
            else if (_rouletteRewardConfigurations.Count < maxAmount)
                return _rouletteRewardConfigurations;
            else
            {
                List<RouletteRewardConfiguration> truncatedRouletteRewardConfigurations = new List<RouletteRewardConfiguration>();
                for (int i = 0; i < maxAmount; i++)
                {
                    truncatedRouletteRewardConfigurations.Add(_rouletteRewardConfigurations[i]);
                }
                return truncatedRouletteRewardConfigurations;
            }
        }

        #endregion
    }
}
