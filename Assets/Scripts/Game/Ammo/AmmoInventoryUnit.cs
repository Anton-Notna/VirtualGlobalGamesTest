using System;

namespace Game.Ammo
{
    public class AmmoInventoryUnit : IAmmoInventoryUnit
    {
        public AmmoInventoryUnit(AmmoType type, int amount)
        {
            Type = type;
            Amount = amount;
        }

        public AmmoType Type { get; private set; }

        public int Amount { get; private set; }

        public int ID => (int)Type;

        public AmmoInventoryUnit Add(int amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Cannot add non-positive ammo amount");

            Amount += amount;
            return this;
        }

        public AmmoInventoryUnit Remove(int amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Cannot remove non-positive ammo amount");

            if (amount > Amount)
                throw new InvalidOperationException("Cannot remove more amount than exists");

            Amount -= amount;
            return this;
        }
    }
}