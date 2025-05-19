using Game.Damages;
using UnityEngine;

namespace Game.Player
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField]
        private Health _health;

        public Health Health => _health;

        public void Init()
        {
            _health.Init();
        }

        public void Teleport(Vector3 playerSpawnPoint)
        {
            
        }
    }
}