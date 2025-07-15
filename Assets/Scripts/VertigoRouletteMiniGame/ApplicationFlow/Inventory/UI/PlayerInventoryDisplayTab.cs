using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI
{
    public class PlayerInventoryDisplayTab : MonoBehaviour
    {
        
        [SerializeField]private Button _button;
        [SerializeField]private TextMeshProUGUI _tabButtonText;
        [SerializeField]private Color _activeTabColor = Color.green;
        [SerializeField]private Color _inactiveTabColor = Color.white;
        private ItemType itemType;
        private PlayerInventoryDisplay playerInventoryDisplay;

#if UNITY_EDITOR
        
        private void OnValidate()
        {
            _button = GetComponent<Button>();
            _tabButtonText = GetComponentInChildren<TextMeshProUGUI>();    
        }
#endif

        public void SetEnabled(bool enabled)
        {
            _button.image.color = enabled ? _activeTabColor : _inactiveTabColor;
        }
        
        public void Initialize(PlayerInventoryDisplay display, ItemType itemType)
        {
            playerInventoryDisplay = display;
            this.itemType = itemType;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnClicked);
            if(itemType == ItemType.None)
                _tabButtonText.text = "All";
            else
                _tabButtonText.text = itemType.ToString();
            
        }

        private void OnClicked()
        {
            playerInventoryDisplay.FilterByTab(itemType);
        }
    }
}