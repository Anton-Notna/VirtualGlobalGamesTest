using Game.Items;
using System;
using UnityEngine;
using Zenject;

namespace Game.Interaction
{
    public class PickableItem : MonoBehaviour, IPickable
    {
        [SerializeField]
        private ItemType _type;

        private ItemsCatalog _catalog;
        private RawItemInventoryUnit? _data;

        [Inject]
        public void Construct(ItemsCatalog catalog) => _catalog = catalog;

        public void SetData(RawItemInventoryUnit data) => _data = data;

        public RawItemInventoryUnit GetData()
        {
            if (_data.HasValue)
                return _data.Value;

            if (_catalog.Get(_type, out var catalogData))
                return catalogData.CreateDefault();

            throw new NullReferenceException($"There is no item typeof {_type} in {_catalog.name}");
        }

        private void Start()
        {
            var type = _data.HasValue ? _data.Value.Type : _type;
            if (_catalog.Get(type, out var catalogData) == false)
                throw new NullReferenceException($"There is no item typeof {type} in {_catalog.name}");

            var gfx = Instantiate(catalogData.Gfx, transform.position, transform.rotation, transform);
            gfx.Collisions = true;
        }
    }
}