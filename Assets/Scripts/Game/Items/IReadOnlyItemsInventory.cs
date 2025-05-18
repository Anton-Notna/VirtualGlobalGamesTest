using DisposableSubscriptions;

namespace Game.Items
{
    public interface IReadOnlyItemsInventory
    {
        public IUpdatableCollection<IItemInventoryUnit> Units { get; }

        public IUpdatable<IItemInventoryUnit> Selected { get; }
    }
}