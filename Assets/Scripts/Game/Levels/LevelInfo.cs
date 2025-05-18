using DisposableSubscriptions;
using Game.Core;
using Game.Damages;
using System;
using System.Collections.Generic;

namespace Game.Levels
{
    public class LevelInfo : ILevelInfo
    {
        private readonly IHealth _player;
        private readonly HashSet<IHealth> _aliveEnemies;
        private readonly List<IDisposable> _subs = new List<IDisposable>();

        public int AliveEnemies => _aliveEnemies.Count;

        public bool AlivePlayer => _player.IsDead() == false;

        public LevelInfo(IHealth player, IEnumerable<IHealth> enemies)
        {
            _player = player;
            foreach (IHealth enemy in enemies)
            {
                enemy.AsUpdatable.Subscribe(ProcessEnemy);
                ProcessEnemy(enemy);
            }
        }

        private void ProcessEnemy(IHealth enemy)
        {
            if (enemy.IsDead())
                _aliveEnemies.Remove(enemy);
            else
                _aliveEnemies.Add(enemy);
        }

        public void Dispose() => _subs.TryDispose();
    }
}