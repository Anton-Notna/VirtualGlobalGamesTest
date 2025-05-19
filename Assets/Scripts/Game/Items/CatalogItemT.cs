using UnityEngine;

namespace Game.Items
{
    public abstract class CatalogItem<T> : CatalogItem where T : ItemInventoryUnit
    {
        [SerializeField]
        private T _default;
        [SerializeField]
        private EquippedItem<T> _equipped;

        public override EquippedItem Equipped => _equipped;

        public override RawItemInventoryUnit CreateDefault() => new RawItemInventoryUnit(default, Key, typeof(T), _default.AsRaw.SubData);
    }
}