using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DisposableSubscriptions.View;
using Game.Items;

namespace Game.UI
{
    public class InventoryViewUnit : UpdatableUnit<InventoryViewUnit, IItemInventoryUnit>
    {
        [SerializeField]
        private ItemsCatalog _catalog;
        [Space]
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private TextMeshProUGUI _subData;
        [SerializeField]
        private Image _selected;

        private IItemInventoryUnit _current;

        public void RefreshSelection(IItemInventoryUnit selected)
        {
            _selected.enabled = _current == selected;
        }

        protected override void Refresh(IItemInventoryUnit unit)
        {
            _current = unit;

            if (_catalog.Get(unit.Type, out var data))
                _name.text = data.name;

            _subData.text = unit.SubDataInfo;
        }
    }
}