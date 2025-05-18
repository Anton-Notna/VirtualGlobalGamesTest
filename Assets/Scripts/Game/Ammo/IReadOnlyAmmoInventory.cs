using DisposableSubscriptions;

namespace Game.Ammo
{
    public interface IReadOnlyAmmoInventory
    {
        public int this[AmmoType type] { get; }

        public IUpdatableCollection<IAmmoInventoryUnit> Units { get; }
    }
}