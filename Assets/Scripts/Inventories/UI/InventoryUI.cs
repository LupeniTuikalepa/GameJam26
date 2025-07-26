using System;
using System.Collections.Generic;
using Crafts;
using LTX;
using UnityEngine;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private GridLayoutGroup layout;
        [SerializeField]
        private RectTransform itemContainer;

        [SerializeField]
        private InventoryItemUI ui;
        [SerializeField]
        private InventoryCellUI cell;

        private Dictionary<string, InventoryItemUI> itemUis;
        private InventoryCellUI[] cells;

        private IInventoryContainer container;
        private Vector2Int currentSize;

        private RectTransform rectTransform;

        private void Awake()
        {
            itemUis = new Dictionary<string, InventoryItemUI>();
            rectTransform = transform as RectTransform;
        }

        public void Bind(IInventoryContainer inventoryContainer)
        {
            if(container != null)
                Unbind(container);

            container = inventoryContainer;

            Inventory inventory = inventoryContainer.GetInventory();
            int width = inventory.Size.x;
            int height = inventory.Size.y;

            currentSize = new Vector2Int(width, height);
            int fullSize = width * height;

            cells = new InventoryCellUI[fullSize];
            for (int i = 0; i < fullSize; i++)
                cells[i] = cell.InstantiatePrefab(layout.transform);

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * layout.cellSize.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * layout.cellSize.y);
        }

        public void Unbind(IInventoryContainer inventoryContainer)
        {
            layout.transform.ClearChildren();
        }


    }
}