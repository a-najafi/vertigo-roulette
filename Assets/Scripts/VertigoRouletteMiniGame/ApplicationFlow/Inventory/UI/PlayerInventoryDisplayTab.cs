using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI
{
    /// <summary>
    /// Represents a filter tab button for inventory categories/types.
    /// Handles UI feedback and tab selection.
    /// </summary>
    public class PlayerInventoryDisplayTab : MonoBehaviour
    {
        #region Serialized Parameters

        /// <summary>
        /// The button component for this tab.
        /// </summary>
        [SerializeField] private Button _button;

        /// <summary>
        /// The text label for the tab button.
        /// </summary>
        [SerializeField] private TextMeshProUGUI _tabButtonText;

        /// <summary>
        /// Color for active/selected tab.
        /// </summary>
        [SerializeField] private Color _activeTabColor = Color.green;

        /// <summary>
        /// Color for inactive/unselected tab.
        /// </summary>
        [SerializeField] private Color _inactiveTabColor = Color.white;

        #endregion

        #region Non Serialized Parameters

        private ItemType itemType;
        private PlayerInventoryDisplay playerInventoryDisplay;

        #endregion

        #region Editor Methods

#if UNITY_EDITOR
        /// <summary>
        /// Ensures references are set in the editor.
        /// </summary>
        private void OnValidate()
        {
            _button = GetComponent<Button>();
            _tabButtonText = GetComponentInChildren<TextMeshProUGUI>();    
        }
#endif

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets this tab's color state to indicate selection.
        /// </summary>
        /// <param name="enabled">True if this tab is selected/active.</param>
        public void SetEnabled(bool enabled)
        {
            _button.image.color = enabled ? _activeTabColor : _inactiveTabColor;
        }
        
        /// <summary>
        /// Initializes the tab with inventory display and item type info.
        /// </summary>
        /// <param name="display">The parent inventory display.</param>
        /// <param name="itemType">The item type/category for this tab.</param>
        public void Initialize(PlayerInventoryDisplay display, ItemType itemType)
        {
            playerInventoryDisplay = display;
            this.itemType = itemType;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnClicked);
            _tabButtonText.text = itemType == ItemType.None ? "All" : itemType.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles button click, triggers filtering in the parent display.
        /// </summary>
        private void OnClicked()
        {
            playerInventoryDisplay.FilterByTab(itemType);
        }

        #endregion
    }
}
