using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DisposableSubscriptions.View;
using Game.Damages;

namespace Game.UI
{
    public class HealthView : UpdatableUnit<HealthView, IHealth>
    {
        [SerializeField]
        private TextMeshProUGUI _amount;
        [SerializeField]
        private Image _fill;

        protected override void Refresh(IHealth unit)
        {
            _amount.text = unit.Current.ToString();
            _fill.fillAmount = unit.Normalized();
        }
    }
}