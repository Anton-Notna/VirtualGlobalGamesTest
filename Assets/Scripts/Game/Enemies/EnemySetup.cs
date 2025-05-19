using Game.Damages;
using System;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemySetup : MonoBehaviour
    {
        [SerializeField]
        private Health _health;

        public IHealth Health => _health;

        public void Init()
        {
            _health.Init();
        }
    }
}