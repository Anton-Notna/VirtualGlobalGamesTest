using DisposableSubscriptions;
using System;
using DisposableSubscriptions.View;
using Game.Items;

namespace Game.UI
{
    public class InventoryView : UpdatableCollectionView<InventoryViewUnit, IItemInventoryUnit>
    {
        private IUpdatable<IItemInventoryUnit> _selected;
        private IDisposable _sub;

        public void Init(IUpdatable<IItemInventoryUnit> selected, IUpdatableCollection<IItemInventoryUnit> units)
        {
            _sub.TryDispose();
            Init(units);

            _selected = selected;
            _sub = _selected.Subscribe(RefreshSelection);
            RefreshSelection(_selected.Current);
        }

        protected override void OnSpawned(InventoryViewUnit unit)
        {
            unit.RefreshSelection(_selected.Current);
        }

        private void RefreshSelection(IItemInventoryUnit selected)
        {
            foreach (var unit in SpawnedUnits.Values)
                unit.RefreshSelection(selected);
        }

        private void OnDestroy() => _sub.TryDispose();
    }
}