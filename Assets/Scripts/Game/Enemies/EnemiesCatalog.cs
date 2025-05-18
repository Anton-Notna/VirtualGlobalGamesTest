using Shared.Catalogs;
using UnityEngine;

namespace Game.Enemies
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Catalogs/Enemies/EnemiesCatalog")]
    public class EnemiesCatalog : Catalog<EnemySetup> { }
}