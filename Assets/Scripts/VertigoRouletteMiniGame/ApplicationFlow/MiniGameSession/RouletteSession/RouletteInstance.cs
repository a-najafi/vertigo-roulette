using System;
using System.Collections;
using System.Collections.Generic;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession
{
    /// <summary>
    /// The roulette state machine's operational states.
    /// </summary>
    public enum ERouletteState
    {
        None = 0,
        Resolved = 1
    }

    /// <summary>
    /// Holds runtime logic, configuration, and result for a single roulette spin session.
    /// </summary>
    public class RouletteInstance
    {
        #region Private Fields

        private RouletteConfigurationBase configuration;
        private List<RouletteRewardConfiguration> rewardConfigurations;
        private ERouletteState state;
        private int resultIndex = -1;

        #endregion

        #region Properties

        /// <summary>
        /// The index of the resolved result (if spun/resolved).
        /// </summary>
        public int ResultIndex => resultIndex;

        /// <summary>
        /// The roulette configuration used for this instance.
        /// </summary>
        public RouletteConfigurationBase Configuration => configuration;

        /// <summary>
        /// List of reward configurations available for this roulette.
        /// </summary>
        public List<RouletteRewardConfiguration> RewardConfigurations => rewardConfigurations;

        /// <summary>
        /// The current state of the roulette spin (None, Resolved, etc).
        /// </summary>
        public ERouletteState State => state;

        /// <summary>
        /// Returns the winning reward configuration after a spin.
        /// </summary>
        public RouletteRewardConfiguration ResultRewardConfiguration
        {
            get
            {
                if (resultIndex < 0 || resultIndex >= rewardConfigurations.Count)
                    return null;
                return rewardConfigurations[resultIndex];
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new RouletteInstance with the provided configuration.
        /// </summary>
        /// <param name="rouletteConfigurationBase">ScriptableObject defining rewards/logic.</param>
        public RouletteInstance(RouletteConfigurationBase rouletteConfigurationBase)
        {
            configuration = rouletteConfigurationBase;
            rewardConfigurations = configuration.GetRewardConfigurations();
            state = ERouletteState.None;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Spins the roulette, selecting a random reward index based on weightings.
        /// Sets state to Resolved and stores the result index.
        /// </summary>
        /// <exception cref="System.Exception">Throws if random index is not valid.</exception>
        public void SpinRoulette()
        {
            int rewardCount = rewardConfigurations.Count;
            float totalRewardWeight = 0;
            float[] weightCaps = new float[rewardCount];

            // Calculate weight caps for each reward (for weighted random selection)
            for (int i = 0; i < rewardCount; i++)
            {
                totalRewardWeight += 1 + rewardConfigurations[i].ProbabilityModifier;
                weightCaps[i] = totalRewardWeight;
            }

            // Roll for random value
            float random = UnityEngine.Random.Range(0, totalRewardWeight);
            int randomIndex = -1;
            for (int i = 0; i < rewardCount; i++)
            {
                if (random < weightCaps[i])
                {
                    randomIndex = i;
                    break;
                }
            }

            if (randomIndex < 0)
                throw new System.Exception("Random index out of range");

            SetResolved(randomIndex);
        }

        /// <summary>
        /// Manually resolve the roulette spin to a specific reward index.
        /// </summary>
        /// <param name="index">The index of the resolved reward.</param>
        /// <exception cref="System.Exception">Throws if index is invalid.</exception>
        public void SetResolved(int index)
        {
            if (index < 0 || index >= rewardConfigurations.Count)
                throw new Exception("Invalid reward index");

            this.resultIndex = index;
            state = ERouletteState.Resolved;
        }

        #endregion
    }
}
