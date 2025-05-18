using UnityEngine;

namespace Game.Items
{
    public abstract class CatalogItem<T> : CatalogItem where T : ItemInventoryUnit
    {
        [SerializeField]
        private T _default;

        public override RawItemInventoryUnit CreateDefault() => new RawItemInventoryUnit(default, Key, typeof(T), _default.AsRaw.SubData);
    }
}