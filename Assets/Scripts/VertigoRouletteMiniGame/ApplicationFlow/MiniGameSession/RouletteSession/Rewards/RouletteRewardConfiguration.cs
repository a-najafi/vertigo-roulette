using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.PropertyAttributes;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards
{
    /// <summary>
    /// Represents a single possible reward (or bomb) on the roulette wheel.
    /// Contains addressable reference to the item, reward amount, and probability modifiers.
    /// </summary>
    [Serializable]
    public class RouletteRewardConfiguration
    {
        #region Serialized Parameters

        /// <summary>
        /// Addressable asset reference to the item definition for this reward.
        /// </summary>
        [SerializeField]
        private AssetReference _itemDefinition;

        /// <summary>
        /// Amount of the reward (negative means "unspecified" or "varies").
        /// </summary>
        [SerializeField]
        private int _amount = -1;

        /// <summary>
        /// Probability weight modifier for this reward.
        /// </summary>
        [SerializeField]
        private float _probabilityModifier = 1f;

        /// <summary>
        /// Whether this reward is a "bomb" (bad result).
        /// </summary>
        [SerializeField]
        private bool _isBomb;

#if UNITY_EDITOR
        /// <summary>
        /// [Editor only] Calculated occurrence chance (display only).
        /// </summary>
        [SerializeField, ReadOnly]
        public float occurenceChance = 0;
#endif

        #endregion

        #region Properties

        /// <summary>
        /// Probability modifier used in weighted random selection.
        /// </summary>
        public float ProbabilityModifier => _probabilityModifier;

        /// <summary>
        /// Amount of the reward.
        /// </summary>
        public int Amount => _amount;

        /// <summary>
        /// Is this reward a bomb (penalty/loss)?
        /// </summary>
        public bool IsBomb => _isBomb;

        /// <summary>
        /// Addressable asset reference to the reward's item definition.
        /// </summary>
        public AssetReference ItemDefinition => _itemDefinition;

        #endregion
    }
}
