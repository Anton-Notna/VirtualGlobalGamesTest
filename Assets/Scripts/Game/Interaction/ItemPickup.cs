using Game.Items;

namespace Game.Interaction
{
    public class ItemPickup : IObjectPickup
    {
        private readonly IItemsInventory _inventory;

        public ItemPickup(IItemsInventory inventory) => _inventory = inventory;

        public bool Pickup(IPickable obj)
        {
            if (obj is PickableItem item == false)
                return false;

            _inventory.Add(item.GetData());
            return true;
        }
    }
}