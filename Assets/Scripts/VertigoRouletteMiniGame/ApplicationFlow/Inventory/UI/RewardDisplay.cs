using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Addressable;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField]private Image _image;
        [SerializeField]private TextMeshProUGUI _text;

        private ItemDefinition itemDefinition;

        public ItemDefinition ItemDefinition => itemDefinition;

        public IEnumerator Initialize(ItemDefinition item, int amount = -1)
        {
            itemDefinition  = item;
            _text.enabled = amount >= 0;
            _text.text = GetAmount(amount);
            var loadItemSpriteHandle = new AssetLoadResult<Sprite>();
            yield return AddressableAssetManager.LoadAsset<Sprite>(item.Icon, loadItemSpriteHandle);
            _image.sprite = loadItemSpriteHandle.Asset;
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