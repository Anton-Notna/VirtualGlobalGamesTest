using System.Collections.Generic;
using UnityEngine;

namespace Shared.Catalogs
{
    public abstract class MappedCatalog<TKey, TValue> : Catalog<TValue> where TValue : ICatalogItem<TKey>
    {
        private Dictionary<TKey, TValue> _items;

        public bool Get(TKey key, out TValue value)
        {
            if (_items == null)
                Init();

            return _items.TryGetValue(key, out value);
        }

#if UNITY_EDITOR
        private void OnValidate() => _items = null;
#endif

        private void Init()
        {
            _items = new Dictionary<TKey, TValue>();
            if (AsList == null)
            {
                Debug.LogWarning($"Empty items in {name}");
                return;
            }

            for (int i = 0; i < AsList.Count; i++)
            {
                var item = AsList[i];
                if (item == null)
                {
                    Debug.LogWarning($"Empty item at {i} in {name}");
                    continue;
                }

                if (_items.ContainsKey(item.Key))
                {
                    Debug.LogWarning($"Duplicate key {item.Key} at {i} in {name}");
                    continue;
                }

                _items.Add(item.Key, item);
            }
        }
    }
}
