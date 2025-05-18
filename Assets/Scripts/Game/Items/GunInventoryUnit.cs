using Game.Items;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class GunInventoryUnit : ItemInventoryUnit<int>
    {
        [SerializeField]
        private int _defaultMagazine;

        public int Magazine 
        {
            get => Data;
            private set => Data = value;
        }
        public override string SubDataInfo => $"Magazine: {Magazine}";

        protected override Type ClassType => typeof(GunInventoryUnit);

        protected override int DefaultData => _defaultMagazine;

        public void Reload(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Cannot reload with non-positive amount");

            Magazine = amount;
        }

        public void Remove(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Cannot remove non-positive amount");

            if (amount > Magazine)
                throw new ArgumentOutOfRangeException("Cannot remove amount greater than current Magazine");

            Magazine -= amount;
        }
    }
}