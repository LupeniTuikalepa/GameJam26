using Inventories;
using UnityEngine;

namespace Crafts
{
    [CreateAssetMenu(fileName = "New CraftRecipe", menuName = "Game/CraftRecipe")]
    public class CraftRecipe : ScriptableObject
    {
        [field: SerializeField]
        public InventoryItemData[] Items { get; private set; }

        [field: SerializeField]
        public InventoryItemData Result { get; private set; }
        [field: SerializeField, Min(1)]
        public int ResultQuantity { get; private set; }

        [field: SerializeField]
        public bool UnlockByDefault { get; private set; }

        public bool Matches(InventoryItem[] items)
        {
            if (items.Length != Items.Length)
                return false;

            for (int i = 0; i < items.Length; i++)
            {
                bool isItemValid = false;
                for (int j = 0; j < Items.Length; j++)
                {
                    if (Items[j] != items[i].Data)
                        continue;

                    isItemValid = true;
                    break;
                }

                if (!isItemValid)
                    return false;
            }

            return true;
        }
    }
}