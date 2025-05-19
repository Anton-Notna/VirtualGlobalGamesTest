using DisposableSubscriptions;
using Game.Damages;
using OmicronCombinedEffects;
using System;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemySetup : MonoBehaviour
    {
        [SerializeField]
        private Health _health;
        [SerializeField]
        private EnemyBehaviour _behaviour;
        [SerializeField]
        private GameObject _aliveRoot;
        [SerializeField]
        private CombinedEffect _deadEffect;

        private IDisposable _sub;

        public IHealth Health => _health;

        public void Init()
        {
            _sub.TryDispose();

            _health.Init();
            _behaviour.Init();
            _sub = _health.AsUpdatable.Subscribe(RefreshAliveState);
        }

        private void OnDestroy()
        {
            _sub.TryDispose();
        }

        private void RefreshAliveState(IHealth health)
        {
            bool alive = health.IsDead() == false;
            if (_aliveRoot.activeSelf == alive)
                return;

            _aliveRoot.SetActive(alive);
            if (alive == false)
                _deadEffect?.Play(transform);

        }
    }

}