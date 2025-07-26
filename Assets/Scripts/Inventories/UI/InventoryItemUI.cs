using System;
using UnityEngine;

namespace Inventories.UI
{
    public class InventoryItemUI : MonoBehaviour
    {
        private InventoryUI parent;
        private void Awake()
        {
            parent = GetComponentInParent<InventoryUI>();
        }

        public void Bind(InventoryItem item)
        {

        }

        public void Unbind(InventoryItem item)
        {
            
        }
    }
}