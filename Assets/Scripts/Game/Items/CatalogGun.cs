using Game.Ammo;
using Game.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Catalogs/Inventory/Gun")]
    public class CatalogGun : CatalogItem<GunInventoryUnit>
    {
        private static readonly List<ItemType> _possibleTypes = new List<ItemType>()
        {
            ItemType.Pistol,
            ItemType.Rifle,
            ItemType.Shotgun,
        };

        [SerializeField]
        private int _maxMagazine = 10;
        [SerializeField]
        private float _delay = 0.1f;
        [SerializeField]
        private int _damage = 1;
        [SerializeField]
        private float _maxDistance = 999f;
        [SerializeField]
        private AmmoType _ammoType;
        [SerializeField]
        private float _reloadDuration = 1f;

        public int MaxMagazine => _maxMagazine;

        public float Delay => _delay;

        public int Damage => _damage;

        public float MaxDistance => _maxDistance;

        public AmmoType Ammo => _ammoType;
        
        public float ReloadDuration => _reloadDuration;

        protected override IEnumerable<ItemType> PossibleTypes => _possibleTypes;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_maxMagazine <= 0)
                _maxMagazine = 1;

            if (_delay < 0)
                _delay = 0;

            if (_damage <= 0)
                _damage = 1;

            if (_maxDistance < 0)
                _maxDistance = 0;

            if (_reloadDuration < 0)
                _reloadDuration = 0;
        }
    }
}