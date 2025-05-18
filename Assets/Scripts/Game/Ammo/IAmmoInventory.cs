namespace Game.Ammo
{
    public interface IAmmoInventory : IReadOnlyAmmoInventory
    {
        public void Add(AmmoType type, int amount);

        public void Remove(AmmoType type, int amount);
    }
}