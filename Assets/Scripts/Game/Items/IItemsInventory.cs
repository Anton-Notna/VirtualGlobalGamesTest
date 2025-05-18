namespace Game.Items
{
    public interface IItemsInventory : IReadOnlyItemsInventory
    {
        public void Add(RawItemInventoryUnit rawItemInventoryUnit);

        public void Select(IItemInventoryUnit unit);

        public bool MoveSelection(bool next);

        public RawItemInventoryUnit ExtractSelected();
    }
}