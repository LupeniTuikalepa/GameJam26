using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{
    [System.Serializable]
    public struct Inventory
    {
        public IReadOnlyList<InventoryItem> Items => items;

        [field: SerializeField]
        private List<InventoryItem> items;

        [field: SerializeField]
        public Vector2Int Size { get; private set; }

        private int[] indices;

        public Inventory(int width, int height)
        {
            Size = new Vector2Int(width, height);
            items = new List<InventoryItem>();
            indices = new int[width * height];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = -1;
        }

        public void AddItem(InventoryItemData data, Vector2Int position)
        {
            items.Add(new InventoryItem(data, position));
            RebuildIndices();
        }


        public bool TryGetItem(string guid, out InventoryItem item)
        {
            int idx = GetIndexOfItem(guid);
            if (idx != -1)
            {

                item = items[idx];
                return true;
            }

            item = default;
            return false;
        }

        public int GetIndexOfItem(InventoryItem item) => GetIndexOfItem(item.Guid);
        public int GetIndexOfItem(string guid)
        {
            int idx = items.FindIndex(0, ctx => ctx.Guid == guid);
            return idx;
        }

        public void RemoveItem(int index) => items.RemoveAt(index);
        public void RemoveItem(InventoryItem item) => RemoveItem(GetIndexOfItem(item));

        public int ToIndex(int x, int y)
        {
            int result = (y * Size.x)+ x;
            return result;
        }


        public void ChangeSize(int width, int height)
        {
            Size = new Vector2Int(width, height);
            indices = new int[width * height];

            RebuildIndices();
        }

        public bool ValidatePosition(InventoryItem item, Vector2Int position, Orientation orientation)
        {
            Vector2Int[] positions = item.GetCells(position, orientation);
            for (int i = 0; i < positions.Length; i++)
            {
                Vector2Int p = positions[i];
                if (p.x < 0 || p.x >= Size.x || p.y < 0 || p.y >= Size.y)
                    return false;

                if (IsCellOccupied(p.x, p.y))
                    return false;
            }

            return true;
        }
        private void RebuildIndices()
        {
            foreach (var item in items)
            {
                int index = GetIndexOfItem(item);
                Vector2Int[] cells = item.GetCells();

                for (int i = 0; i < cells.Length; i++)
                {
                    int cellIndex = ToIndex(cells[i].x, cells[i].y);
                    indices[cellIndex] = index;
                }
            }
        }

        public bool IsCellOccupied(int x, int y)
        {
            int index = ToIndex(x, y);

            return indices[index] != -1;
        }
    }
}