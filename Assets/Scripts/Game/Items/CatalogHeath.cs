using Game.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Catalogs/Inventory/HeathBox")]
    public class CatalogHeath : CatalogItem<SimpleInventoryUnit>
    {
        private static readonly List<ItemType> _possibleTypes = new List<ItemType>()
        {
            ItemType.HeathBoxSmall,
            ItemType.HeathBoxBig,
        };

        [SerializeField]
        private int _restoreHealthAmount = 1;

        protected override IEnumerable<ItemType> PossibleTypes => _possibleTypes;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_restoreHealthAmount <= 1)
                _restoreHealthAmount = 1;
        }
    }
}