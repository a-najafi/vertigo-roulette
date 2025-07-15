using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Addressable;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI
{
    /// <summary>
    /// Displays a single reward item, showing its icon and quantity in the inventory UI.
    /// </summary>
    public class RewardDisplay : MonoBehaviour
    {
        #region Serialized Parameters

        /// <summary>
        /// Image component to display the item's icon.
        /// </summary>
        [SerializeField] private Image _image;

        /// <summary>
        /// Text label for showing the amount/quantity.
        /// </summary>
        [SerializeField] private TextMeshProUGUI _text;

        #endregion

        #region Non Serialized Parameters

        private ItemDefinition itemDefinition;

        #endregion

        #region Properties

        /// <summary>
        /// The item definition this reward display currently represents.
        /// </summary>
        public ItemDefinition ItemDefinition => itemDefinition;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this reward display with a specific item definition and amount.
        /// Loads the item's sprite asynchronously.
        /// </summary>
        /// <param name="item">The item definition to display.</param>
        /// <param name="amount">The amount of the item to show. If negative, hides the amount label.</param>
        /// <returns>Coroutine enumerator.</returns>
        public IEnumerator Initialize(ItemDefinition item, int amount = -1)
        {
            itemDefinition = item;
            _text.enabled = amount >= 0;
            _text.text = GetAmount(amount);

            var loadItemSpriteHandle = new AssetLoadResult<Sprite>();
            yield return AddressableAssetManager.LoadAsset<Sprite>(item.Icon, loadItemSpriteHandle);
            _image.sprite = loadItemSpriteHandle.Asset;
        }

        /// <summary>
        /// Formats an amount into a human-readable string (e.g., "x1k" for thousands).
        /// </summary>
        /// <param name="amount">The quantity to format.</param>
        /// <returns>Formatted amount string.</returns>
        public string GetAmount(int amount)
        {
            string amountString;
            if (amount > 1000)
            {
                amount /= 1000;
                amountString = "x" + amount.ToString() + "k";
            }
            else
            {
                amountString = "x" + amount.ToString();
            }
            return amountString;
        }

        #endregion
    }
}
