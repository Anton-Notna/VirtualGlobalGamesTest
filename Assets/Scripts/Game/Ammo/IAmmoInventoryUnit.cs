using DisposableSubscriptions;

namespace Game.Ammo
{
    public interface IAmmoInventoryUnit : IIdentifiable
    {
        public AmmoType Type { get; }

        public int Amount { get; }
    }
}