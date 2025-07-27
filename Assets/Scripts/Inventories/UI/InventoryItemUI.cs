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
        private static Vector3[] corners = new Vector3[4];


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
            bool first = true;
            Vector3 min = Vector3.zero;
            Vector3 max = Vector3.zero;

            for (int i = 0; i < cells.Length; i++)
            {
                var coord = cells[i];

                //Debug.Log($"item {item.Data.name} occupies Cell {coord}");
                var cell = parent.GetCellForCoord(coord.x, coord.y);

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

            if (transform is RectTransform rectTransform)
            {

                Vector3 size = max - min;
                Vector3 pivotOffset = new Vector2(rectTransform.pivot.x * size.x, rectTransform.pivot.y * size.y);

                rectTransform.sizeDelta = size;
                rectTransform.anchoredPosition = (min + pivotOffset);
            }
        }

        public void Unbind(InventoryItem item)
        {

        }

    }
}