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

        public GridLayoutGroup Layout => layout;

        [SerializeField]
        private RectTransform itemContainer;

        [SerializeField]
        private InventoryItemUI itemUIPrefab;
        [SerializeField]
        private InventoryCellUI cell;

        private Dictionary<string, InventoryItemUI> itemUis;
        private InventoryCellUI[] cells;

        private IInventoryContainer container;
        private Inventory currentInventory;

        private RectTransform rectTransform;

        private void Awake()
        {
            itemUis = new Dictionary<string, InventoryItemUI>();
            rectTransform = transform as RectTransform;
        }

        public void Open(IInventoryContainer inventoryContainer)
        {
            if (container != null)
                Close(container, false);

            container = inventoryContainer;

            currentInventory = inventoryContainer.GetInventory();
            int width = currentInventory.Size.x;
            int height = currentInventory.Size.y;

            Debug.Log(currentInventory.Size);
            int fullSize = width * height;

            cells = new InventoryCellUI[fullSize];
            for (int i = 0; i < fullSize; i++)
                cells[i] = cell.InstantiatePrefab(layout.transform);

            float horizontalSize = width * layout.cellSize.x +
                                   (width - 1) * layout.spacing.x + layout.padding.left +
                                   layout.padding.right;
            float verticalSize = height * layout.cellSize.y +
                                 (height - 1) * layout.spacing.y +
                                 layout.padding.top + layout.padding.bottom;

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalSize);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, verticalSize);

            Canvas.ForceUpdateCanvases();

            foreach (InventoryItem item in currentInventory.Items)
            {
                InventoryItemUI itemUI = itemUIPrefab.InstantiatePrefab(itemContainer);
                itemUI.Bind(item);
                itemUis.Add(item.Guid, itemUI);
            }
        }


        public InventoryCellUI GetCellForCoord(int x, int y)
        {
            int index = currentInventory.ToIndex(x, y);
            if(index != -1)
                return cells[index];

            return null;
        }

        public Vector3 GetPositionForCoord(int x, int y)
        {
            int index = currentInventory.ToIndex(x, y);
            if (index < 0 || index > layout.transform.childCount)
            {
                return transform.position;
            }

            if (layout.transform.GetChild(index) is RectTransform t)
                return t.position;

            return transform.position;
        }

        public void Close(IInventoryContainer inventoryContainer, bool apply)
        {
            layout.transform.ClearChildren();

            foreach ((string guid, InventoryItemUI inventoryItemUI) in itemUis)
            {
                if(currentInventory.TryGetItem(guid, out InventoryItem item))
                    inventoryItemUI.Unbind(item);

                inventoryItemUI.DestroyGameObject();
            }

            itemContainer.ClearChildren();
            itemUis.Clear();

            if(apply)
                inventoryContainer.SetInventory(currentInventory);

            currentInventory = default;
        }


    }
}