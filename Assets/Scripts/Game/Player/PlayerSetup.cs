using Game.Damages;
using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField]
        private Health _health;

        public Health Health => _health;

        internal void Init()
        {
            throw new NotImplementedException();
        }

        internal void Teleport(Vector3 playerSpawnPoint)
        {
            throw new NotImplementedException();
        }
    }
}