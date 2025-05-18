using DisposableSubscriptions;
using Game.Items;
using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class ItemHolder : MonoBehaviour
    {
        private IItemsInventory _inventory;
        private IFactory<EquippedItem, EquippedItem> _factory;
        private ItemsCatalog _catalog;
        private IDisposable _sub;
        private IItemInventoryUnit _currentUnit;
        private EquippedItem _current;

        [Inject]
        public void Construct(IFactory<EquippedItem, EquippedItem> factory, ItemsCatalog catalog, IItemsInventory inventory)
        {
            _inventory = inventory;
            _factory = factory;
            _catalog = catalog;
        }

        public void TryPrimaryAction()
        {
            _current?.DoPrimaryAction();
        }

        public void TrySecondaryAction()
        {
            _current?.DoSecondaryAction();
        }

        private void Start()
        {
            _sub.TryDispose();
            _sub = _inventory.Selected.Subscribe(Refresh);
            Refresh(_inventory.Selected.Current);
        }

        private void Refresh(IItemInventoryUnit unit)
        {
            if (_currentUnit == unit)
                return;

            _currentUnit = unit;
            if (_current != null)
            {
                _current.Clear();
                Destroy(_current.gameObject);
                _current = null;
            }

            if (_currentUnit != null)
            {
                if (_catalog.Get(_currentUnit.Type, out var data) == false)
                    throw new NullReferenceException($"There is no data for type {_currentUnit.Type} in {_catalog.name}");

                _current = _factory.Create(data.Equipped);
                _current.transform.SetParent(transform);
                _current.transform.SetPositionAndRotation(transform.position, transform.rotation);
                _current.Init(unit);
            }
        }

        private void OnDestroy()
        {
            _sub.TryDispose();
        }
    }
}