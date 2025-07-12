using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility.Addressable;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI
{
    public class ZoneDisplayComponent : MonoBehaviour
    {
        
        private int zoneDisplayIndex;
        private ZoneInstance zoneInstance;
        
        [SerializeField]private Image _zoneImage;
        [SerializeField]private TextMeshProUGUI _zoneIndexText;
        
        

        public virtual IEnumerator Initialize(ZoneInstance zoneInstance, int zoneDisplayIndex)
        {
            this.zoneDisplayIndex = zoneDisplayIndex;
            this.zoneInstance = zoneInstance;

            AssetReferenceAtlasedSprite spriteRef = zoneInstance.ZoneConfiguration.ZoneSpriteActive;

            switch (zoneInstance.ZoneState)
            {
                case EZoneState.Active:
                    spriteRef = zoneInstance.ZoneConfiguration.ZoneSpriteActive;
                    break;
                case EZoneState.Win:
                    spriteRef = zoneInstance.ZoneConfiguration.ZoneSpriteResolved;
                    break;
                case EZoneState.Lose:
                    spriteRef = zoneInstance.ZoneConfiguration.ZoneSpriteComing;
                    break;
            }

            yield return AddressableAssetManager.LoadAsset<Sprite>(spriteRef, (sprite =>
            {
                _zoneImage.sprite = sprite;
            }));

            
            _zoneIndexText.text = zoneInstance.ZoneIndex.ToString();
            yield return null;
        }
    }
}