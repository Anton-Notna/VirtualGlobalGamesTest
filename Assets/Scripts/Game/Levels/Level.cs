using UnityEngine;

namespace Game.Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawnPoint;
        [SerializeField]
        private Transform[] _enemySpawnPoints;

        public Vector3 PlayerSpawnPoint => _playerSpawnPoint.position;

        public int EnemiesSpawnPointsCount => _enemySpawnPoints.Length;

        public Vector3 GetEnemySpawnPoint(int index) => _enemySpawnPoints[index].position;

        private void OnDrawGizmos()
        {
            if (_playerSpawnPoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(PlayerSpawnPoint, 0.5f);
            }

            if (_enemySpawnPoints != null)
            {
                Gizmos.color = Color.red;

                for (int i = 0; i < _enemySpawnPoints.Length; i++)
                {
                    Transform origin = _enemySpawnPoints[i];
                    if (origin != null)
                        Gizmos.DrawSphere(origin.position, 0.5f);
                }
            }
        }
    }
}