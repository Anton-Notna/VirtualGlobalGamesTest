using Game.Ammo;
using Game.Items;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class GameHUD : Window
    {
        [SerializeField]
        private HealthView _heath;
        [SerializeField]
        private AmmoView _ammo;
        [SerializeField]
        private InventoryView _inventory;

        private PlayerSetup _playerSetup;
        private IReadOnlyAmmoInventory _ammoInventory;
        private IReadOnlyItemsInventory _itemsInventory;

        [Inject]
        public void Construct(PlayerSetup player, IReadOnlyAmmoInventory ammo, IReadOnlyItemsInventory items)
        {
            _playerSetup = player;
            _ammoInventory = ammo;
            _itemsInventory = items;
        }

        private void Start()
        {
            _heath.Init(_playerSetup.Health.AsUpdatable);
            _ammo.Init(_ammoInventory.Units);
            _inventory.Init(_itemsInventory.Selected, _itemsInventory.Units);
        }
    }
}