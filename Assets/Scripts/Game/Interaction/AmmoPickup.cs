using Game.Ammo;

namespace Game.Interaction
{
    public class AmmoPickup : IObjectPickup
    {
        private readonly IAmmoInventory _inventory;

        public AmmoPickup(IAmmoInventory inventory) => _inventory = inventory;

        public bool Pickup(IPickable obj)
        {
            if (obj is AmmoBox ammo == false)
                return false;

            _inventory.Add(ammo.Type, ammo.Amount);
            return true;
        }
    }
}