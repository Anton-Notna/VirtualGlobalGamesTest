using Game.Player;
using System;
using Zenject;

namespace Game.Items
{
    public class EquippedHealthBox : EquippedItem<SimpleInventoryUnit>
    {
        private ItemsCatalog _catalog;
        private PlayerSetup _player;
        private IItemsInventory _inventory;
        private SimpleInventoryUnit _current;
        private CatalogHeath _data;

        [Inject]
        public void Construct(ItemsCatalog itemsCatalog, IItemsInventory itemsInventory, PlayerSetup playerSetup)
        {
            _catalog = itemsCatalog;
            _player = playerSetup;
            _inventory = itemsInventory;
        }

        public override void DoPrimaryAction()
        {
            _player.Health.Heal(_data.RestoreHealthAmount);
            _inventory.ExtractSelected();
        }

        protected override void Init(SimpleInventoryUnit item)
        {
            _current = item;

            if (_catalog.Get(_current.Type, out var data) == false)
                throw new NullReferenceException($"There is no {_current.Type} in {_catalog.name}");

            if ((data is CatalogHeath heathData) == false)
                throw new InvalidCastException($"{data.name} is not CatalogHeath");

            _data = heathData;
        }
    }
}