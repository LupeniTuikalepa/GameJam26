using UnityEngine;

namespace Inventories
{
    [CreateAssetMenu(fileName = "New Inventory Item", menuName = "Game/Inventory Item", order = 0)]
    public class InventoryItemData : ScriptableObject
    {
        [field: SerializeField]
        public Vector2Int[] OccupiedSpace { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }
    }
}