namespace Game.Items
{
    public abstract class ItemInventoryUnit<TData> : ItemInventoryUnit
    {
        private const string Key = "data0";

        protected abstract TData DefaultData { get; }

        protected TData Data
        {
            get => GetSubData(Key, out TData magazine) ? magazine : DefaultData;
            set => SetSubData(Key, value);
        }

        // Serialize default values
        protected override void OnBeforeSerialize()
        {
            if (ContainsSubData(Key) == false)
                Data = DefaultData;
        }
    }
}