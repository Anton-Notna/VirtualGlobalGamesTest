using DisposableSubscriptions;
using System;
using DisposableSubscriptions.View;
using Game.Items;

namespace Game.UI
{
    public class InventoryView : UpdatableCollectionView<InventoryViewUnit, IItemInventoryUnit>
    {
        private IDisposable _sub;

        public void Init(IUpdatable<IItemInventoryUnit> selected, IUpdatableCollection<IItemInventoryUnit> units)
        {
            _sub.TryDispose();
            Init(units);

            _sub = selected.Subscribe(RefreshSelection);
            RefreshSelection(selected.Current);
        }

        private void RefreshSelection(IItemInventoryUnit selected)
        {
            for (int i = 0; i < SpawnedUnits.Count; i++)
                SpawnedUnits[i].RefreshSelection(selected);
        }

        private void OnDestroy() => _sub.TryDispose();
    }
}