using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class InventoryItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        public CanvasGroup canvasGroup;
        [SerializeField]
        private Image image;
        [SerializeField]
        private TextMeshProUGUI quantityText;

        private Vector2 offset;

        public void Sync(InventoryItem item)
        {
            image.sprite = item.Data.Icon;
            quantityText.text = item.Quantity.ToString();

        }

        public void Unbind(InventoryItem item)
        {

        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            transform.DOKill(true);
            transform.DOScale(Vector3.one * 1.1f, .2f);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            transform.DOKill(true);
            transform.DOScale(Vector3.one, .2f);
        }
    }
}