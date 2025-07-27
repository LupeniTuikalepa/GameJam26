using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class CraftElementUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private Image[] icons;
        [SerializeField]
        private Image result;
        [SerializeField]
        private TextMeshProUGUI quantity;

        [SerializeField]
        private GameObject cantBeCraftedEffect;

        [SerializeField]
        private CanvasGroup canvasGroup;

        private CraftUI craftUI;

        public void Craft() => craftUI.Craft(this);

        public void Sync(InventoryItemData craftableItem, CraftUI craftUi)
        {
            craftUI = craftUi;
            for (int i = 0; i < icons.Length; i++)
            {
                Image icon = icons[i];
                if (i < craftableItem.Recipe.Length)
                {
                    icon.transform.parent.gameObject.SetActive(true);
                    icon.sprite = craftableItem.Recipe[i].Icon;
                }
                else
                    icon.transform.parent.gameObject.SetActive(false);
            }

            result.sprite = craftableItem.Icon;
            quantity.text = craftableItem.CraftQuantity.ToString();
        }

        public void UpdateCraftableState(InventoryItemData data, Inventory inventory)
        {
            bool canBeCrafted = data.CanBeCrafted(inventory);
            cantBeCraftedEffect.SetActive(!canBeCrafted);
            canvasGroup.interactable = canBeCrafted;
            if (!canBeCrafted)
                canvasGroup.alpha = .5f;
            else
                canvasGroup.alpha = 1;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (canvasGroup.interactable)
                canvasGroup.DOFade(.8f, .2f);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (canvasGroup.interactable)
                canvasGroup.DOFade(1f, .2f);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) => Craft();
    }
}