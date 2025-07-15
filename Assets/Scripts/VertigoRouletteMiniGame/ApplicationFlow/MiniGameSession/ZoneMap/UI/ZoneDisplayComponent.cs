using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.UI;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI
{
    /// <summary>
    /// Handles the display logic for a zone: background, index, roulette, and final reward UI.
    /// </summary>
    public class ZoneDisplayComponent : MonoBehaviour
    {
        #region Serialized Parameters

        [SerializeField] private Image _zoneImage;
        [SerializeField] private TextMeshProUGUI _zoneIndexText;
        [SerializeField] private RouletteDisplay _rouletteDisplay;
        [SerializeField] private GameObject _zoneRewardDisplay;

        #endregion

        #region Non-Serialized Parameters

        /// <summary>
        /// The zone instance this display represents.
        /// </summary>
        private ZoneInstance zoneInstance;

        #endregion

        #region Properties

        /// <summary>
        /// The zone instance currently displayed.
        /// </summary>
        public ZoneInstance ZoneInstance => zoneInstance;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this display for a new ZoneInstance, sets the background sprite and index text.
        /// </summary>
        public virtual IEnumerator Initialize(ZoneInstance zoneInstance)
        {
            _zoneRewardDisplay.SetActive(false);
            _rouletteDisplay.gameObject.SetActive(false);

            this.zoneInstance = zoneInstance;

            // Pick the correct sprite for the zone state
            AssetReferenceAtlasedSprite spriteRef = zoneInstance.ZoneConfiguration.ZoneSpriteActive;
            switch (zoneInstance.ZoneState)
            {
                case EZoneState.None:
                    spriteRef = zoneInstance.ZoneConfiguration.ZoneSpriteActive;
                    break;
                case EZoneState.Win:
                    spriteRef = zoneInstance.ZoneConfiguration.ZoneSpriteResolved;
                    break;
                case EZoneState.Lose:
                    spriteRef = zoneInstance.ZoneConfiguration.ZoneSpriteComing;
                    break;
            }

            // Load and set the zone background sprite
            var result = new AssetLoadResult<Sprite>();
            yield return AddressableAssetManager.LoadAsset<Sprite>(spriteRef, result);
            _zoneImage.sprite = result.Asset;

            // Set the zone index
            _zoneIndexText.text = zoneInstance.ZoneIndex.ToString();
            yield return null;
        }

        /// <summary>
        /// Initializes the roulette display for the current zone.
        /// </summary>
        public virtual IEnumerator InitializeRoulette()
        {
            yield return _rouletteDisplay.Initialize(zoneInstance);
            _rouletteDisplay.gameObject.SetActive(true);
        }

        /// <summary>
        /// Spins the roulette and lands on the specified outcome index.
        /// </summary>
        public IEnumerator SpinRoulette(int outComeIndex)
        {
            yield return _rouletteDisplay.Spin(outComeIndex);
        }

        /// <summary>
        /// Displays the final reward after the roulette spin, showing reward details.
        /// </summary>
        public IEnumerator DisplayFinalReward()
        {
            _rouletteDisplay.gameObject.SetActive(false);
            _zoneRewardDisplay.SetActive(true);

            // Defensive checks
            if (zoneInstance == null)
                throw new NullReferenceException("No Zone instance assigned");
            if (zoneInstance.RouletteInstance == null)
                throw new NullReferenceException("No Roulette instance assigned");
            if (zoneInstance.RouletteInstance.ResultRewardConfiguration == null)
                throw new NullReferenceException("No Result Reward configuration assigned");
            if (zoneInstance.RouletteInstance.ResultRewardConfiguration.ItemDefinition == null)
                throw new NullReferenceException("No Item Reward configuration assigned");

            // Load the item definition and display it as a reward
            var result = new AssetLoadResult<ItemDefinition>();
            yield return AddressableAssetManager.LoadAsset<ItemDefinition>(
                zoneInstance.RouletteInstance.ResultRewardConfiguration.ItemDefinition, result);

            RewardDisplay _zoneRewardDisplayComponent = _zoneRewardDisplay.GetComponentInChildren<RewardDisplay>();

            if (_zoneRewardDisplayComponent != null)
                yield return _zoneRewardDisplayComponent.Initialize(
                    result.Asset, 
                    zoneInstance.RouletteInstance.ResultRewardConfiguration.Amount
                );
        }

        #endregion
    }
}
