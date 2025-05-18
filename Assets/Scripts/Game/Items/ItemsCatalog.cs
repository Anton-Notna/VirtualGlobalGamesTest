using Shared.Catalogs;
using UnityEngine;

namespace Game.Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Catalogs/Inventory/ItemsCatalog")]
    public class ItemsCatalog : MappedCatalog<ItemType, CatalogItem> { }
}