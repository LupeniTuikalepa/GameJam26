using Inventories;
using UnityEngine;

namespace Crafts
{
    public static class CraftManager
    {
        private static CraftRecipe[] recipes;

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            recipes = Resources.LoadAll<CraftRecipe>("Recipes");
        }

        public static bool TryCraft(InventoryItem[] items, out CraftRecipe recipe)
        {
            for (int i = 0; i < recipes.Length; i++)
            {
                CraftRecipe r = recipes[i];
                if (r.Matches(items))
                {
                    recipe = r;
                    return true;
                }
            }

            recipe = null;
            return false;
        }
    }
}