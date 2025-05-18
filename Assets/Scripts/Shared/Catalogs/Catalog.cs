using System.Collections.Generic;
using UnityEngine;

namespace Shared.Catalogs
{
    public abstract class Catalog<T> : ScriptableObject
    {
        [SerializeField]
        private List<T> _items = new List<T>();

        public IReadOnlyList<T> AsList => _items;
    }
}
