using AdvTriggers;
using Game.Damages;
using Game.Player;
using OmicronFSM;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Enemies
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField]
        private StateMachine _mind;
        [SerializeField]
        private Health _health;
        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private Trigger _findPlayer;
        [SerializeField]
        private Trigger _lostPlayer;
        [SerializeField]
        private Shooter _shooter;
        [SerializeField]
        private float _randomWalkDelay = 5f;
        [SerializeField]
        private float _randomWalkRadius = 5f;

        private PlayerSetup _player;

        [Inject]
        public void Construct(PlayerSetup player) => _player = player;

        public void Init()
        {
            _mind?.Reset();

            var idle = new RandomWalk(this);
            var attack = new Attack(this);
            var dead = new Dead(this);

            _mind = new MachineBuilder(gameObject)
                .State(idle, "Idle").AsEnter()
                .State(attack, "Attack")
                .State(dead, "Dead")

                .Transit(() => _health.IsDead(), "Zero health").FromOthersTo(dead)
                .Transit(() => _findPlayer.Contains(_player.transform.position), "See player").From(idle).To(attack)
                .Transit(() => _lostPlayer.Contains(_player.transform.position) == false, "Lost player").From(attack).To(idle)

                .Build();
        }

        private void Update() => _mind?.Tick();

        private class RandomWalk : EnemyState
        {
            private float _nextGeneration;

            public RandomWalk(EnemyBehaviour enemyContext) : base(enemyContext) { }

            public override void Enter() => _nextGeneration = 0;

            public override void Update()
            {
                if (Time.time < _nextGeneration)
                    return;

                _nextGeneration = Time.time + EnemyContext._randomWalkDelay;
                Vector3 offset = UnityEngine.Random.insideUnitSphere * EnemyContext._randomWalkRadius;
                offset.z = 0;
                EnemyContext._agent.SetDestination(Context.Transform.position + offset);
            }

            public override void Exit() => EnemyContext._agent.SetDestination(Context.Transform.position);
        }

        private class Attack : EnemyState
        {
            public Attack(EnemyBehaviour enemyContext) : base(enemyContext) { }

            public override void Update()
            {
                EnemyContext._agent.SetDestination(EnemyContext._player.transform.position);
                if (EnemyContext._shooter.CanShoot())
                    EnemyContext._shooter.Shoot();
            }

            public override void Exit() => EnemyContext._agent.SetDestination(Context.Transform.position);
        }

        private class Dead : EnemyState
        {
            public Dead(EnemyBehaviour enemyContext) : base(enemyContext) { }

            public override void Enter() => EnemyContext._agent.isStopped = true;

            public override void Exit() => EnemyContext._agent.isStopped = false;
        }

        private abstract class EnemyState : State
        {
            protected EnemyBehaviour EnemyContext { get; }

            protected EnemyState(EnemyBehaviour enemyContext) => EnemyContext = enemyContext;
        }
    }

}