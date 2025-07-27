using System;
using UnityEngine;

namespace Inventories
{
    [System.Serializable]
    public class InventoryItem
    {

        [field: SerializeField]
        public InventoryItemData Data { get; private set; }

        [field: SerializeField, Min(0)]
        public int Quantity { get; private set; }


        public InventoryItem(InventoryItemData data, int quantity)
        {
            Data = data;
            Quantity = quantity;
        }

        public void Increase(int quantity) => Quantity += quantity;
        public void Decrease(int quantity) => Quantity -= quantity;
    }
}