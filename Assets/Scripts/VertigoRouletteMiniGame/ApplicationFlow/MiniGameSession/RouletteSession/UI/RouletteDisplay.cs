using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utility.Addressable;
using Utility.UI;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.UI
{
    /// <summary>
    /// Handles roulette UI display, reward slots, and spin animations for the minigame session.
    /// </summary>
    public class RouletteDisplay : MonoBehaviour
    {
        #region Serialized Parameters

        /// <summary>
        /// The main roulette wheel image.
        /// </summary>
        [SerializeField] private Image _rouletteImage;

        /// <summary>
        /// The pin or marker image on the roulette wheel.
        /// </summary>
        [SerializeField] private Image _roulettePinImage;

        /// <summary>
        /// RadialLayout component responsible for arranging reward slots around the wheel.
        /// </summary>
        [SerializeField] private RadialLayout _rewardRadialLayout;

        /// <summary>
        /// Prefab for each reward display UI element.
        /// </summary>
        [SerializeField] private GameObject _rewardDisplayPrefab;

        /// <summary>
        /// How many full spins to do before landing on the result.
        /// </summary>
        [SerializeField] private int _initialSpins = 5;

        /// <summary>
        /// Duration of the spin animation in seconds.
        /// </summary>
        [SerializeField] private float _spinDuration = 4f;

        /// <summary>
        /// Animation ease curve for the spin.
        /// </summary>
        [SerializeField] private Ease _spinEase = Ease.OutQuart;

        #endregion

        #region Non Serialized Parameters

        /// <summary>
        /// Runtime dictionary of reward slot displays, keyed by their index.
        /// </summary>
        private Dictionary<int, RewardDisplay> _rewardDisplays = new Dictionary<int, RewardDisplay>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the roulette wheel display for the given zone, loading sprites and reward slots.
        /// </summary>
        /// <param name="zoneInstance">The gameplay zone containing roulette configuration.</param>
        public IEnumerator Initialize(ZoneInstance zoneInstance)
        {
            // Load roulette wheel sprite
            var loadRouletteSpriteHandle = new AssetLoadResult<Sprite>();
            yield return AddressableAssetManager.LoadAsset<Sprite>(zoneInstance.ZoneConfiguration.RouletteSprite, loadRouletteSpriteHandle);
            _rouletteImage.sprite = loadRouletteSpriteHandle.Asset;

            // Load roulette pin sprite
            var loadRoulettePinSpriteHandle = new AssetLoadResult<Sprite>();
            yield return AddressableAssetManager.LoadAsset<Sprite>(zoneInstance.ZoneConfiguration.RoulettePinSprite, loadRoulettePinSpriteHandle);
            _roulettePinImage.sprite = loadRoulettePinSpriteHandle.Asset;

            // Create reward slots
            if (zoneInstance.RouletteInstance != null)
            {
                _rewardRadialLayout.enabled = false;
                for (int i = 0; i < zoneInstance.RouletteInstance.RewardConfigurations.Count; i++)
                {
                    yield return AddRewardDisplay(zoneInstance.RouletteInstance.RewardConfigurations[i], i);
                }
                _rewardRadialLayout.enabled = true;
            }
        }

        /// <summary>
        /// Spins the roulette and lands the given reward slot at 12 o'clock (top).
        /// </summary>
        /// <param name="finalOutcomeIndex">Index to land at the top after spin.</param>
        public IEnumerator Spin(int finalOutcomeIndex)
        {
            float anglePerSlot = 360f / _rewardRadialLayout.slotCount;
            float targetAngle = (finalOutcomeIndex * anglePerSlot);

            // Offset so the specified slot lands at the top (12:00)
            if (targetAngle > 90)
                targetAngle = targetAngle - 90;
            else
                targetAngle += 270;

            float totalRotation = (_initialSpins * 360f) + (360f - targetAngle);

            yield return _rewardRadialLayout.transform.DOLocalRotate(
                new Vector3(0, 0, totalRotation), _spinDuration, RotateMode.FastBeyond360)
                .SetEase(_spinEase)
                .WaitForCompletion();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Adds (or reuses) a reward display at the specified index with the given configuration.
        /// Loads icon addressable as needed.
        /// </summary>
        /// <param name="rouletteRewardConfiguration">Reward data/config.</param>
        /// <param name="index">Slot index.</param>
        protected IEnumerator AddRewardDisplay(RouletteRewardConfiguration rouletteRewardConfiguration, int index)
        {
            var loadItemDefinitionHandle = new AssetLoadResult<ItemDefinition>();
            yield return AddressableAssetManager.LoadAsset<ItemDefinition>(rouletteRewardConfiguration.ItemDefinition, loadItemDefinitionHandle);

            RewardDisplay rewardDisplay;
            if (_rewardDisplays.ContainsKey(index))
            {
                rewardDisplay = _rewardDisplays[index];
            }
            else
            {
                GameObject gameObject = Instantiate(_rewardDisplayPrefab, _rewardRadialLayout.transform, false);
                rewardDisplay = gameObject.GetComponent<RewardDisplay>();
                _rewardDisplays.Add(index, rewardDisplay);
            }
            yield return rewardDisplay.Initialize(loadItemDefinitionHandle.Asset, rouletteRewardConfiguration.Amount);
        }

        #endregion
    }
}
