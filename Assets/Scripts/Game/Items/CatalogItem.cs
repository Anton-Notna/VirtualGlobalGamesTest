using Shared.Catalogs;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.Player;

namespace Game.Items
{
    public abstract class CatalogItem : ScriptableObject, ICatalogItem<ItemType>
    {
        [SerializeField]
        private ItemType _type;
        [SerializeField]
        private ItemGfx _gfx;
        [SerializeField]
        private EquippedItem _equipped;

        public ItemType Key => _type;

        public ItemGfx Gfx => _gfx;

        public EquippedItem Equipped => _equipped;

        protected abstract IEnumerable<ItemType> PossibleTypes { get; }

        public abstract RawItemInventoryUnit CreateDefault();

        protected virtual void OnValidate()
        {
            if (PossibleTypes.Contains(_type) == false)
                _type = PossibleTypes.FirstOrDefault();

            if (CreateDefault().Type != _type)
                Debug.LogError($"Invalid type of \"CreateDefault()\" : {CreateDefault().Type}, expected: {_type}");
        }
    }
}