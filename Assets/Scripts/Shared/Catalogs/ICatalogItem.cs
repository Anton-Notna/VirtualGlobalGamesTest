namespace Shared.Catalogs
{
    public interface ICatalogItem<TKey>
    {
        public TKey Key { get; }
    }
}
