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

        [Inject]
        public void Construct(PlayerSetup player, IReadOnlyAmmoInventory ammo, IReadOnlyItemsInventory items)
        {
            _heath.Init(player.Health.AsUpdatable);
            _ammo.Init(ammo.Units);
            _inventory.Init(items.Units);
        }
    }
}