using DisposableSubscriptions;
using OmicronDamages;
using UnityEngine;

namespace Game.Damages
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _initialHp = 100;
        [SerializeField]
        private Damageable[] _damageableParts;

        private readonly Updatable<Health> _updatable = new Updatable<Health>();

        private bool _inited;

        public int Current { get; private set; }

        public int Max { get; private set; }

        public IUpdatable<IHealth> AsUpdatable => _updatable;

        public void Init() => Init(_initialHp);

        public void Init(int current)
        {
            Max = _initialHp;
            Current = Mathf.Clamp(current, 0, Max);

            if (_damageableParts != null)
            {
                for (int i = 0; i < _damageableParts.Length; i++)
                    _damageableParts[i].SetProvider(this);
            }

            _inited = true;

            _updatable.Update(this);
        }

        public bool TakeDamage(DamagePoint point, Damage data)
        {
            if (_inited == false)
                return false;

            if (this.IsDead() == false)
                return false;

            Current -= data.Amount;
            if (Current < 0)
                Current = 0;

            _updatable.Update(this);

            return true;
        }
    }
}