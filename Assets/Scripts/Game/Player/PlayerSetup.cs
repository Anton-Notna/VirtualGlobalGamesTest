using Game.Damages;
using SUPERCharacter;
using UnityEngine;

namespace Game.Player
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField]
        private Health _health;
        [SerializeField]
        private SUPERCharacterAIO _movement;
        [SerializeField]
        private PlayerControl _controls;

        public Health Health => _health;

        public void Init()
        {
            _health.Init();
            _controls.Enabled = true;
        }

        public void Teleport(Vector3 playerSpawnPoint)
        {
            _movement.PausePlayer(PauseModes.FreezeInPlace);
            transform.position = playerSpawnPoint;
            _movement.UnpausePlayer();
        }
    }
}