using Game.Ammo;
using Game.Damages;
using System;
using UnityEngine;
using Zenject;

namespace Game.Items
{
    public class EquippedGun : EquippedItem<GunInventoryUnit>
    {
        [SerializeField]
        private Shooter _shooter;
        [SerializeField]
        private LayerMask _mask;
        [Space]
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private string _reloadTrigger;
        [SerializeField]
        private string _aimBool;

        private ItemsCatalog _catalog;
        private IAmmoInventory _ammo;
        private GunInventoryUnit _current;
        private CatalogGun _data;
        private float _reloadEnd;

        [Inject]
        public void Construct(ItemsCatalog itemsCatalog, IAmmoInventory ammo)
        {
            _catalog = itemsCatalog;
            _ammo = ammo;
        }

        public override void DoPrimaryAction()
        {
            if (Time.time < _reloadEnd)
                return;

            if (_shooter.CanShoot() == false)
                return;

            // Shoot
            if (_current.Magazine > 0)
            {
                _shooter.Shoot();
                _current.Remove(1);
                return;
            }

            // Reload
            if (_ammo[_data.Ammo] > 0)
            {
                int toTransit = Mathf.Min(_data.MaxMagazine, _ammo[_data.Ammo]);
                _ammo.Remove(_data.Ammo, toTransit);
                _current.Reload(toTransit);
                _animator.SetTrigger(_reloadTrigger);
                _reloadEnd = Time.time + _data.ReloadDuration;
            }
        }

        public override void DoSecondaryAction()
        {
            _animator.SetBool(_aimBool, _animator.GetBool(_aimBool) == false);
        }

        protected override void Init(GunInventoryUnit item)
        {
            _current = item;

            if (_catalog.Get(_current.Type, out var data) == false)
                throw new NullReferenceException($"There is no {_current.Type} in {_catalog.name}");

            if ((data is CatalogGun gunData) == false)
                throw new InvalidCastException($"{data.name} is not about guns :(");

            _data = gunData;
            _shooter.Init(_data.Delay, _data.Damage, _data.MaxDistance, _mask);
        }
    }
}