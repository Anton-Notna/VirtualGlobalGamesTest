using OmicronCombinedEffects;
using OmicronDamages;
using UnityEngine;

namespace Game.Damages
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        private float _minDelay;
        [SerializeField] 
        private int _damage = 1;
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private LayerMask _mask;
        [Space]
        [Header("Optional:")]
        [SerializeField]
        private Transform _shootEffectOrigin;
        [SerializeField]
        private CombinedEffect _shoot;
        [SerializeField]
        private CombinedEffect _impact;

        private float _nextFireTime;

        public void Init(float minDelay, int damage, float maxDistance, LayerMask mask)
        {
            _minDelay = minDelay;
            _damage = Mathf.Max(damage, 1);
            _maxDistance = maxDistance;
            _mask = mask;

            _nextFireTime = Time.time - _minDelay;
        }

        public bool CanShoot() => Time.time >= _nextFireTime;

        public void Shoot()
        {
            if (CanShoot() == false)
                return;

            _nextFireTime = Time.time + _minDelay;

            _shoot?.Play(_shootEffectOrigin == null ? transform : _shootEffectOrigin);

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _maxDistance, _mask))
            {
                _impact?.Play(hit.point, hit.normal);

                if (hit.collider.TryGetComponent<IDamageable<Damage>>(out var target))
                {
                    var damage = new Damage()
                    {
                        Amount = _damage,
                    };

                    var point = new DamagePoint()
                    {
                        Hit = new Ray(hit.point, hit.normal),
                        Direction = new Ray(hit.point, transform.forward)
                    };

                    target.TakeDamage(point, damage);
                }

            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_damage < 1)
                _damage = 1;
        }
#endif
    }

}