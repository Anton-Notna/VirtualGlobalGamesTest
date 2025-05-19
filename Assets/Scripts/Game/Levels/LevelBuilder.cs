using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game.Core;
using Game.Damages;
using Game.Player;
using Game.Enemies;

using Object = UnityEngine.Object;
using Random = System.Random;

namespace Game.Levels
{
    public class LevelBuilder : ILevelBuilder, IDisposable
    {
        private readonly LevelsCatalog _levels;
        private readonly IFactory<Level, Level> _factory;
        private readonly IFactory<EnemySetup, EnemySetup> _enemyFactory;

        private readonly PlayerSetup _player;
        private readonly EnemiesCatalog _enemies;
        private readonly Random _random = new Random();
        private readonly List<GameObject> _spawnedObjects = new List<GameObject>();

        public LevelBuilder(LevelsCatalog levels, IFactory<Level, Level> factory, IFactory<EnemySetup, EnemySetup> enemyFactory, PlayerSetup player, EnemiesCatalog enemiesCatalog)
        {
            _levels = levels;
            _factory = factory;
            _enemyFactory = enemyFactory;
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

            _player.Init();
            _player.Teleport(level.PlayerSpawnPoint);

            List<IHealth> enemies = new List<IHealth>();
            if (_enemies.AsList.Count > 0)
            {
                for (int i = 0; i < level.EnemiesSpawnPointsCount; i++)
                {
                    EnemySetup enemyPrefab = _enemies.AsList[_random.Next(0, _enemies.AsList.Count)];
                    EnemySetup enemy = _enemyFactory.Create(enemyPrefab);
                    enemy.transform.SetLocalPositionAndRotation(level.GetEnemySpawnPoint(i), Quaternion.identity);
                    enemy.Init();
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