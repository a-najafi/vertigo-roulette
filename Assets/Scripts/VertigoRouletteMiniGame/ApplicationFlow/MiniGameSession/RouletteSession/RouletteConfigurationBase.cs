using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession
{
    /// <summary>
    /// Abstract base class for roulette wheel reward configuration.
    /// Subclass for different roulette modes or reward calculation rules.
    /// </summary>
    public class RouletteConfigurationBase : ScriptableObject
    {
        #region Public Methods

        /// <summary>
        /// Gets the list of reward configurations for this roulette setup.
        /// Implement in child classes to provide different logic for generating or filtering rewards.
        /// </summary>
        /// <param name="maxAmount">Optional: maximum number of rewards to return, or -1 for unlimited.</param>
        /// <returns>List of reward configurations (can include bombs, weighted, etc.).</returns>
        public virtual List<RouletteRewardConfiguration> GetRewardConfigurations(int maxAmount = -1)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}