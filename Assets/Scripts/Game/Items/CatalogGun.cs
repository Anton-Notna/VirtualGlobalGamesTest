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

        protected override IEnumerable<ItemType> PossibleTypes => _possibleTypes;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_maxMagazine <= 1)
                _maxMagazine = 1;
        }
    }
}