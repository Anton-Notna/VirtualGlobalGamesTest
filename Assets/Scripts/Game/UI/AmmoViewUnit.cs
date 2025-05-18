using TMPro;
using UnityEngine;
using DisposableSubscriptions.View;
using Game.Ammo;

namespace Game.UI
{
    public class AmmoViewUnit : UpdatableUnit<AmmoViewUnit, IAmmoInventoryUnit>
    {
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private TextMeshProUGUI _amount;

        protected override void Refresh(IAmmoInventoryUnit unit)
        {
            _name.text = unit.Type.ToString();
            _amount.text = unit.Amount.ToString();
        }
    }
}