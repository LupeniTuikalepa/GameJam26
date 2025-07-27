using UnityEngine;
using UnityEngine.Pool;

namespace Inventories
{
    [CreateAssetMenu(fileName = "New Inventory Item", menuName = "Game/Inventory Item", order = 0)]
    public class InventoryItemData : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        [field: SerializeField]
        public InventoryItemData[] Recipe { get; private set; }

        [field: SerializeField]
        public int CraftQuantity { get; private set; } = 0;

        public bool IsCraftable => Recipe.Length > 0 && CraftQuantity > 0;

        public void Consume(Inventory inventory)
        {
            for (int i = 0; i < Recipe.Length; i++)
                inventory.RemoveItem(Recipe[i]);
        }

        public bool CanBeCrafted(Inventory inventory)
        {
            using (DictionaryPool<InventoryItemData, int>.Get(out var dic))
            {
                for (int i = 0; i < Recipe.Length; i++)
                {
                    InventoryItemData ingredient = Recipe[i];
                    if (!dic.TryAdd(ingredient, 1))
                        dic[ingredient]++;
                }

                foreach ((InventoryItemData data, int qtt) in dic)
                {
                    if (inventory.GetItemQuantity(data) < qtt)
                        return false;
                }

            }

            return true;
        }
    }
}