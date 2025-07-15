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
    public class RouletteDisplay : MonoBehaviour
    {
        [SerializeField]private Image _rouletteImage;
        [SerializeField]private Image _roulettePinImage;
        [SerializeField]private RadialLayout _rewardRadialLayout;
        
        [SerializeField] private GameObject _rewardDisplayPrefab;
        [SerializeField] private int _initialSpins = 5;
        [SerializeField] private float _spinDuration = 4;
        [SerializeField] private  Ease _spinEase = Ease.OutQuart;
        private Dictionary<int,RewardDisplay> _rewardDisplays = new Dictionary<int,RewardDisplay>();

        public IEnumerator Initialize(ZoneInstance zoneInstance)
        {
            var loadRouletteSpriteHandle = new AssetLoadResult<Sprite>();
            yield return AddressableAssetManager.LoadAsset<Sprite>(zoneInstance.ZoneConfiguration.RouletteSprite,
                loadRouletteSpriteHandle);
            _rouletteImage.sprite = loadRouletteSpriteHandle.Asset;
            
            var loadRoulettePinSpriteHandle = new AssetLoadResult<Sprite>();
            yield return AddressableAssetManager.LoadAsset<Sprite>(zoneInstance.ZoneConfiguration.RoulettePinSprite,
                loadRoulettePinSpriteHandle);
            _roulettePinImage.sprite = loadRoulettePinSpriteHandle.Asset;
                
            if (zoneInstance.RouletteInstance != null)
            {
                _rewardRadialLayout.enabled = false;
                List<IEnumerator> addRewardDisplayRoutines = new List<IEnumerator>();
                for (int i = 0; i < zoneInstance.RouletteInstance.RewardConfigurations.Count; i++)
                {
                    yield return AddRewardDisplay(zoneInstance.RouletteInstance.RewardConfigurations[i], i);
                }

                //yield return this.WaitForAll(addRewardDisplayRoutines);
                _rewardRadialLayout.enabled = true;
            }
            
        }

        protected IEnumerator AddRewardDisplay(RouletteRewardConfiguration rouletteRewardConfiguration, int index)
        {

            //Action<ItemDefinition> onItemDefinitionLoaded = 
            var loadItemDefinitionHandle = new AssetLoadResult<ItemDefinition>();
            yield return AddressableAssetManager.LoadAsset<ItemDefinition>(rouletteRewardConfiguration.ItemDefinition,
                loadItemDefinitionHandle);
            
            RewardDisplay rewardDisplay;
            if (_rewardDisplays.ContainsKey(index))
            {
                //Debug.Log("Reward Display Found for " + index + " using it");
                rewardDisplay = _rewardDisplays[index];
            }
            else
            {
                //Debug.Log("Reward Display Not Found for " + index + " adding new reward display");
                GameObject gameObject = Instantiate(_rewardDisplayPrefab, _rewardRadialLayout.transform, false);
                rewardDisplay = gameObject.GetComponent<RewardDisplay>();    
                _rewardDisplays.Add(index,rewardDisplay);
            }
            yield return rewardDisplay.Initialize(loadItemDefinitionHandle.Asset, rouletteRewardConfiguration.Amount);
        }

        
        
        /// <summary>
        /// Spins and lands the target index at 12 o'clock (top).
        /// </summary>
        public IEnumerator Spin(int finalOutcomeIndex)
        {
            
            float anglePerSlot = 360f / _rewardRadialLayout.slotCount;
            float targetAngle = (finalOutcomeIndex * anglePerSlot);
            if (targetAngle > 90)
                targetAngle = targetAngle - 90;
            else
                targetAngle += 270;
            //Debug.Log("for index " + finalOutcomeIndex + " target angle " + targetAngle);
            float totalRotation = (_initialSpins * 360f) + (360f - targetAngle);

            yield return _rewardRadialLayout.transform.DOLocalRotate(new Vector3(0, 0, totalRotation), _spinDuration, RotateMode.FastBeyond360)
                .SetEase(_spinEase).WaitForCompletion();
            
            
        }
        
        
        
        
    }
}