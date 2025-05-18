using DisposableSubscriptions;

namespace Game.Items
{
    public interface IItemInventoryUnit : IIdentifiable
    {
        public ItemType Type { get; }

        public string SubDataInfo { get; }
    }
}