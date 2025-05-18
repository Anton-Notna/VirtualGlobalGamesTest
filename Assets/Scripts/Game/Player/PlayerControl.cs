using Game.Interaction;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField]
        private PlayerRotation _rotation;
        [SerializeField]
        private ObjectPickups _pickup;
        [SerializeField]
        private ObjectsThrow _throw;
        [SerializeField]
        private ItemHolder _holder;

        private IPlayerInput _input;

        [Inject]
        public void Construct(IPlayerInput input)
        {
            _input = input;
        }

        private void Update()
        {
            if (_input == null)
                return;

            if (_input.Enabled == false)
                return;

            _rotation.UpdateRotation(_input.Rotation);

            if (_input.Pickup)
                _pickup.TryPickup();

            if (_input.Throw)
                _throw.TryThrow();

            if (_input.PrimaryAction)
                _holder.TryPrimaryAction();

            if (_input.SecondaryAction)
                _holder.TrySecondaryAction();
        }
    }
}