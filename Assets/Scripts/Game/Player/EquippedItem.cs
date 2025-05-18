using Game.Items;
using UnityEngine;

namespace Game.Player
{
    public abstract class EquippedItem : MonoBehaviour
    {
        public abstract void Init(IItemInventoryUnit item);

        public virtual void Clear() { }

        public virtual void DoPrimaryAction() { }

        public virtual void DoSecondaryAction() { }
    }
}