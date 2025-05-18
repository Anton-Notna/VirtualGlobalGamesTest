using Game.Items;
using System;

namespace Game.Player
{
    public abstract class EquippedItem<T> : EquippedItem where T : IItemInventoryUnit
    {
        public override void Init(IItemInventoryUnit item)
        {
            if (item is T typedItem == false)
                throw new InvalidOperationException($"{gameObject.name} cannot process item typeof {item.GetType()}");

            Init(typedItem);
        }

        protected abstract void Init(T item);
    }
}