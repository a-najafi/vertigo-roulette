using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.UI
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField]private Image _image;
        [SerializeField]private TextMeshProUGUI _text;

        public IEnumerator Initialize(ItemDefinition item, int amount = -1)
        {
            _text.enabled = amount >= 0;
            _text.text = GetAmount(amount);
            
            yield return AddressableAssetManager.LoadAsset<Sprite>(item.Icon,
                (rewardIcon => _image.sprite = rewardIcon));
        }

        public string GetAmount(int amount)
        {
            string amountString;
            if (amount > 1000)
            {
                amount /= 1000;
                amountString = "x"+amount.ToString()+"k";
            }
            else
            {
                amountString  = "x" + amount.ToString();
            }
            return amountString;
        }

    }
}