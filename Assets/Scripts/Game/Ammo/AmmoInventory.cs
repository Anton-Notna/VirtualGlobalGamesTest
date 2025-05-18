using Cysharp.Threading.Tasks;
using DisposableSubscriptions;
using Shared.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Ammo
{
    public class AmmoInventory : IAmmoInventory, ISerializableObject<Dictionary<AmmoType, int>>
    {
        private readonly UpdatableCollection<AmmoInventoryUnit> _units = new UpdatableCollection<AmmoInventoryUnit>();

        public IUpdatableCollection<IAmmoInventoryUnit> Units => _units;

        public int this[AmmoType type] => _units.Get((int)type, out var unit) && unit.Exists ? unit.Current.Amount : 0;

        public void SetData(Dictionary<AmmoType, int> data)
        {
            _units.RemoveAll();
            foreach (var item in data)
                Add(item.Key, item.Value);
        }

        public Dictionary<AmmoType, int> GetData()
            => _units.Collection
            .Where(u => u.Exists)
            .Select(u => u.Current)
            .ToDictionary(u => u.Type, u => u.Amount);

        public void Add(AmmoType type, int amount)
        {
            if (_units.Get((int)type, out var unit))
                _units.UpdateUnit(unit.Current.Add(amount));
            else
                _units.UpdateUnit(new AmmoInventoryUnit(type, amount));
        }

        public void Remove(AmmoType type, int amount)
        {
            if (_units.Get((int)type, out var unit) == false)
                throw new NullReferenceException($"There is no ammo typeof {type}");

            _units.UpdateUnit(unit.Current.Remove(amount));
        }
    }
}