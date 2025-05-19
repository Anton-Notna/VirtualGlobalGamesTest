using OmicronDamages;
using UnityEngine;

namespace Game.Damages
{
    public class Damageable : MonoBehaviour, IDamageable<Damage>
    {
        [SerializeField]
        private int _incomeDamageBonus = 10;

        private IDamageable<Damage> _provider;

        public void SetProvider(IDamageable<Damage> provider) => _provider = provider;

        public bool TakeDamage(DamagePoint point, Damage data)
        {
            if (_provider == null)
                return false;

            data.Amount += _incomeDamageBonus;
            return _provider.TakeDamage(point, data);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_incomeDamageBonus < 0)
                _incomeDamageBonus = 0;
        }
#endif
    }

}