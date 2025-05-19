using UnityEngine;

namespace Game.Items
{
    public abstract class EquippedItem : MonoBehaviour
    {
        public abstract void Init(IItemInventoryUnit item);

        public virtual void Clear() { }

        public virtual void DoPrimaryAction() { }

        public virtual void DoSecondaryAction() { }
    }
}