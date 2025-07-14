using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.UI;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI
{
    public class ZoneDisplayComponent : MonoBehaviour
    {
        
        private ZoneInstance zoneInstance;

        

        [SerializeField]private Image _zoneImage;
        [SerializeField]private TextMeshProUGUI _zoneIndexText;
        [SerializeField]private RouletteDisplay _rouletteDisplay;
        
        [SerializeField]private GameObject _zoneRewardDisplay;
        
        public ZoneInstance ZoneInstance => zoneInstance;
        public virtual IEnumerator Initialize(ZoneInstance zoneInstance)
        {
            _zoneRewardDisplay.SetActive(false);
            _rouletteDisplay.gameObject.SetActive(false);
            
            this.zoneInstance = zoneInstance;

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

            yield return AddressableAssetManager.LoadAsset<Sprite>(spriteRef,
                sprite =>
                {
                    _zoneImage.sprite = sprite;
                });

         

            
            _zoneIndexText.text = zoneInstance.ZoneIndex.ToString();
            yield return null;
        }

        public virtual IEnumerator InitializeRoulette()
        {
            
            yield return _rouletteDisplay.Initialize(zoneInstance);
            _rouletteDisplay.gameObject.SetActive(true);
            
        }

        public IEnumerator SpinRoulette(int outComeIndex)
        {
            yield return _rouletteDisplay.Spin(outComeIndex);
        }

        public IEnumerator DisplayFinalReward()
        {
            _rouletteDisplay.gameObject.SetActive(false);
            _zoneRewardDisplay.SetActive(true);
            if(zoneInstance == null)
                throw new NullReferenceException("No Zone instance assigned");
            if(zoneInstance.RouletteInstance == null)
                throw new NullReferenceException("No Roulette instance assigned");
            if(zoneInstance.RouletteInstance.ResultRewardConfiguration == null)
                throw new NullReferenceException("No Result Reward configuration assigned");
            if(zoneInstance.RouletteInstance.ResultRewardConfiguration.ItemDefinition  == null)
                throw new NullReferenceException("No Item Reward configuration assigned");
            
            
            yield return AddressableAssetManager.LoadAsset<ItemDefinition>(
                zoneInstance.RouletteInstance.ResultRewardConfiguration.ItemDefinition,
                definition =>
                {
                    RewardDisplay _zoneRewardDisplayComponent =
                        _zoneRewardDisplay.GetComponentInChildren<RewardDisplay>();
                    if(_zoneRewardDisplayComponent != null)
                        StartCoroutine(_zoneRewardDisplayComponent.Initialize(definition,zoneInstance.RouletteInstance.ResultRewardConfiguration.Amount));
                });
        }
    }
}