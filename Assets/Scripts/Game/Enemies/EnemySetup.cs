using Game.Damages;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemySetup : MonoBehaviour
    {
        [SerializeField]
        private Health _health;

        public Health Health => _health;
    }
}