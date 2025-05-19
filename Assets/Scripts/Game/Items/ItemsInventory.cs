using Cysharp.Threading.Tasks;
using DisposableSubscriptions;
using Shared.Storage;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Items
{
    public class ItemsInventory : IItemsInventory, ISerializableObject<RawItemsInventory>
    {
        private readonly UpdatableCollection<ItemInventoryUnit> _units = new UpdatableCollection<ItemInventoryUnit>();
        private readonly Updatable<IItemInventoryUnit> _selected = new Updatable<IItemInventoryUnit>();

        public IUpdatableCollection<IItemInventoryUnit> Units => _units;

        public IUpdatable<IItemInventoryUnit> Selected => _selected;

        public void Add(RawItemInventoryUnit raw)
        {
            var unit = ItemInventoryUnit.FromRaw(raw, ForceUpdate);
            _units.UpdateUnit(unit);

            if (_units.Collection.Count == 1)
                Select(_units.Get(unit.ID).Current);
        }

        public RawItemsInventory GetData()
            => new RawItemsInventory(
                _selected.Exists ? _selected.Current.ID : null,
                _units.Collection.Select(u => u.Current.AsRaw));

        public void Select(IItemInventoryUnit unit)
        {
            if (unit != null && _units.Contains(unit.ID) == false)
                throw new ArgumentException("Cannot select unit that doesn't exist in collection");

            _selected.Update(unit);
        }

        public void SetData(RawItemsInventory data)
        {
            _units.RemoveAll();

            if (data.Items == null)
            {
                _selected.Update(null);
                return;
            }

            for (int i = 0; i < data.Items.Count; i++)
                _units.UpdateUnit(ItemInventoryUnit.FromRaw(data.Items[i], ForceUpdate));

            _selected.Update(_units.Collection.Select(u => u.Current).FirstOrDefault(u => u.ID == data.SelectedId));
        }

        public RawItemInventoryUnit ExtractSelected()
        {
            if (_selected.Exists == false)
                throw new NullReferenceException("There is no selected unit");

            int id = _selected.Current.ID;
            var data = _units.Get(id).Current.AsRaw;
            bool newSelected = MoveSelection(true);
            _units.Remove(id);
            if (newSelected == false)
                _selected.Update(null);
            return data;
        }

        public bool MoveSelection(bool next)
        {
            if (_selected.Exists == false)
            {
                if (_units.Collection.Count == 0)
                    return false;

                _selected.Update(_units.Collection[0].Current);
                return true;
            }

            if (_units.Collection.Count == 1 && _selected.Current.ID == _units.Collection[0].Current.ID)
                return false;

            int index = GetIndex(_selected.Current.ID);
            index += next ? 1 : -1;
            if (index < 0)
                index = _units.Collection.Count - 1;
            else if (index >= _units.Collection.Count)
                index = 0;

            _selected.Update(_units.Collection[index].Current);
            return true;
        }

        private int GetIndex(int id)
        {
            for (int i = 0; i < _units.Collection.Count; i++)
            {
                if (_units.Collection[i].Current.ID == id)
                    return i;
            }

            throw new ArgumentException($"There is no item with id {id}");
        }


        private void ForceUpdate(ItemInventoryUnit unit)
        {
            _units.UpdateUnit(unit);

            if (_selected.Exists && _selected.Current.ID == unit.ID)
                _selected.Update(unit);
        }
    }
}