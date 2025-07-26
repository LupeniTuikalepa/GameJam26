using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class InventoryItemUI : MonoBehaviour
    {
        private InventoryUI parent;

        [SerializeField]
        private Image image;

        private void Awake()
        {
            parent = GetComponentInParent<InventoryUI>();
            image = GetComponent<Image>();
        }

        public void Bind(InventoryItem item)
        {
            image.sprite = item.Data.Icon;
            transform.eulerAngles = item.Orientation switch
            {
                Orientation.Right => Vector3.zero,
                Orientation.Left => Vector3.forward * 180,
                Orientation.Up => Vector3.forward * 90,
                Orientation.Down => Vector3.forward * -90,
                _ => throw new ArgumentOutOfRangeException()
            };

            Vector2Int[] cells = item.GetCells();

            Vector2Int min = item.Position;
            Vector2Int max = item.Position;

            Bounds bounds = new Bounds(parent.GetPositionForCoord(item.Position.x, item.Position.y), Vector3.zero);

            for (int i = 0; i < cells.Length; i++)
            {
                int x = cells[i].x;
                int y = cells[i].y;

                if (min.x < x)
                    min.x = x;
                if (max.x > x)
                    max.x = x;
                if (min.y < y)
                    min.y = y;
                if (max.y > y)
                    max.y = y;

                bounds.Encapsulate(parent.GetPositionForCoord(x, y));
            }

            int deltaX = Mathf.Abs(max.x - min.x) + 1;
            int deltaY = Mathf.Abs(max.y - min.y) + 1;
            Vector2 cellSize = parent.Layout.cellSize;
            Vector2 spacing = parent.Layout.spacing;

            RectTransform rectTransform = transform as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                    deltaX * cellSize.x + (deltaX - 1) * spacing.x);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                    deltaY * cellSize.y + (deltaY - 1) * spacing.y);
            }

            if (rectTransform != null)
                rectTransform.position = bounds.center;
        }

        public void Unbind(InventoryItem item)
        {

        }

    }
}