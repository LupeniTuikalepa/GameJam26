using Inventories;

namespace Crafts
{
    public interface IInventoryContainer
    {
        public Inventory GetInventory();
        public void SetInventory(Inventory inventory);
    }
}