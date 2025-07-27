using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private static Vector3[] corners = new Vector3[4];


        [SerializeField]
        private Image image;
        [SerializeField]
        private CanvasGroup canvasGroup;

        private bool beingDragged;


        private RectTransform rectTransform;
        private Vector2 offset;
        private InventoryUI inventoryUI;
        private Canvas canvas;

        private void Awake()
        {
            inventoryUI = GetComponentInParent<InventoryUI>();
            canvas = GetComponentInParent<Canvas>();
            rectTransform = transform as RectTransform;
        }



        public void Bind(InventoryItem item)
        {
            image.sprite = item.Data.Icon;

            Vector2Int[] cells = item.GetCells();
            bool first = true;
            Vector3 min = Vector3.zero;
            Vector3 max = Vector3.zero;

            for (int i = 0; i < cells.Length; i++)
            {
                var coord = cells[i];

                var cell = inventoryUI.GetCellForCoord(coord.x, coord.y);

                cell.RectTransform.GetWorldCorners(corners);

                for (int j = 0; j < 4; j++)
                {
                    Vector3 worldCorner = corners[j];
                    Vector3 localCorner = transform.InverseTransformPoint(worldCorner);

                    if (first)
                    {
                        min = localCorner;
                        max = localCorner;
                        first = false;
                    }
                    else
                    {
                        min = Vector3.Min(min, localCorner);
                        max = Vector3.Max(max, localCorner);
                    }
                }
            }
                Vector3 size = max - min;
                Vector3 pivotOffset = new Vector2(rectTransform.pivot.x * size.x, rectTransform.pivot.y * size.y);

                rectTransform.sizeDelta = size;
                rectTransform.anchoredPosition = (min + pivotOffset);

                if (image.transform is RectTransform imageTransform)
                {
                    imageTransform.eulerAngles = item.Orientation switch
                    {
                        Orientation.Right => Vector3.zero,
                        Orientation.Left => Vector3.forward * 180,
                        Orientation.Up => Vector3.forward * 90,
                        Orientation.Down => Vector3.forward * -90,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    imageTransform.sizeDelta = item.Orientation switch
                    {
                        Orientation.Right or Orientation.Left => size,
                        Orientation.Up or Orientation.Down => new Vector2(size.y, size.x),
                        _ => Vector2.zero
                    };
                }

        }

        public void Unbind(InventoryItem item)
        {

        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            transform.position = InventoryDragUtilities.GetPos(eventData.position, canvas, eventData.enterEventCamera);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (!beingDragged)
            {
                beingDragged = true;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out offset);

                inventoryUI.BeginDrag(this, eventData);
            }
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1;
            if (beingDragged)
            {
                beingDragged = false;
                inventoryUI.EndDrag(this, eventData);
            }
        }

    }
}