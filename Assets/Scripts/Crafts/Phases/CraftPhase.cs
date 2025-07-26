using System;
using System.Collections.Generic;
using Inventories;
using UnityEngine;

namespace Crafts.Phases
{
    public class CraftPhase
    {

        public event Action<InventoryItemData[]> OnItemChanges;
        public event Action<CraftRecipe> OnNewRecipe;
        public event Action<Inventory> OnInventoryChanges;

        public event Action<bool> OnEnd;

        public Inventory Inventory { get; private set; }

        public InventoryItemData[] currentItems;
        public CraftRecipe CurrentRecipe { get; private set; }
        public IInventoryContainer Target { get; private set; }

        public void Confirm(Vector2Int position)
        {
            if (CurrentRecipe != null)
            {
                InventoryItemData result = CurrentRecipe.Result;
                Inventory.AddItem(result, position);

                Target.SetInventory(Inventory);
                OnEnd?.Invoke(true);
            }
            else
            {
                OnEnd?.Invoke(false);
            }

        }

        public void Cancel()
        {
            OnEnd?.Invoke(false);
        }
    }
}