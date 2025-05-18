using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game.Core;
using Game.Damages;

using Object = UnityEngine.Object;
using Random = System.Random;
using Game.Player;
using Game.Enemies;

namespace Game.Levels
{
    public class LevelBuilder : ILevelBuilder, IDisposable
    {
        private readonly LevelsCatalog _levels;
        private readonly IFactory<Level, Level> _factory;
        private readonly PlayerSetup _player;
        private readonly EnemiesCatalog _enemies;
        private readonly Random _random = new Random();
        private readonly List<GameObject> _spawnedObjects = new List<GameObject>();

        public LevelBuilder(LevelsCatalog levels, IFactory<Level, Level> factory, PlayerSetup player, EnemiesCatalog enemiesCatalog)
        {
            _levels = levels;
            _factory = factory;
            _player = player;
            _enemies = enemiesCatalog;
        }

        public ILevelInfo Build(int levelIndex)
        {
            Cleanup();

            if (_levels.AsList.Count == 0)
                throw new NullReferenceException($"Empty LevelsCatalog {_levels.name}");

            Level level = _factory.Create(_levels.AsList[levelIndex % _levels.AsList.Count]);
            _spawnedObjects.Add(level.gameObject);

            _player.Teleport(level.PlayerSpawnPoint);

            List<Health> enemies = new List<Health>();
            if (_enemies.AsList.Count > 0)
            {
                for (int i = 0; i < level.EnemiesSpawnPointsCount; i++)
                {
                    EnemySetup enemyPrefab = _enemies.AsList[_random.Next(_enemies.AsList.Count - 1)];
                    EnemySetup enemy = Object.Instantiate(enemyPrefab, level.GetEnemySpawnPoint(i), Quaternion.identity);
                    _spawnedObjects.Add(enemy.gameObject);
                    enemies.Add(enemy.Health);
                }
            }
            else
            {
                Debug.LogWarning($"Empty EnemiesCatalog {_enemies.name}");
            }

            return new LevelInfo(_player.Health, enemies);
        }

        public void Cleanup()
        {
            if (_spawnedObjects.Count > 0)
            {
                for (int i = 0; i < _spawnedObjects.Count; i++)
                {
                    GameObject @object = _spawnedObjects[i];
                    if (@object != null)
                        Object.Destroy(@object);
                }

                _spawnedObjects.Clear();
            }
        }

        public void Dispose() => Cleanup();
    }
}