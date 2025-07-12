using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility.Addressable;
using Utility.UI;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.UI
{
    public class RouletteDisplay : MonoBehaviour
    {
        [SerializeField]private Image _rouletteImage;
        [SerializeField]private Image _roulettePinImage;
        [SerializeField]private RadialLayout _rewardRadialLayout;
        
        [SerializeField] private GameObject _rewardDisplayPrefab;
        
        

        public IEnumerator Initialize(ZoneInstance zoneInstance)
        {
            yield return AddressableAssetManager.LoadAsset<Sprite>(zoneInstance.ZoneConfiguration.RouletteSprite,
                sprite =>
                {
                    _rouletteImage.sprite = sprite;
                });
            
            
            yield return AddressableAssetManager.LoadAsset<Sprite>(zoneInstance.ZoneConfiguration.RoulettePinSprite,
                sprite =>
                {
                    _roulettePinImage.sprite = sprite;
                });
        }
        
    }
}