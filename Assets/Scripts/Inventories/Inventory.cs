using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventories
{
    [System.Serializable]
    public class Inventory
    {
        public event Action<Inventory> OnUpdate;
        public IReadOnlyList<InventoryItem> Items => items;

        [field: SerializeField]
        private List<InventoryItem> items;

        public Inventory()
        {
            items = new List<InventoryItem>();
        }

        public void RemoveItem(InventoryItemData data, int quantity = 1)
        {
            foreach (var item in items)
            {
                if (item.Data == data)
                {
                    item.Decrease(quantity);
                    break;
                }
            }

            items.RemoveAll(ctx => ctx.Quantity <= 0);
            OnUpdate?.Invoke(this);
        }
        public void AddItem(InventoryItemData data, int quantity = 1)
        {
            foreach (var item in items)
            {
                if (item.Data == data)
                {
                    item.Increase(quantity);
                    break;
                }
            }

            items.Add(new InventoryItem(data, quantity));
            OnUpdate?.Invoke(this);
        }

        public int GetItemQuantity(InventoryItemData data)
        {
            foreach (var item in items)
            {
                if (item.Data == data)
                    return item.Quantity;
            }

            return 0;
        }

        public InventoryItem GetItem(InventoryItemData data) => items.FirstOrDefault(item => item.Data == data);
    }
}