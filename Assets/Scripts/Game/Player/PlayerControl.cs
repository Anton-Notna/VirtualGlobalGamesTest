using Game.Interaction;
using Game.Items;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField]
        private string _primaryButton = "Fire1";
        [SerializeField]
        private string _secondaryButton = "Fire2";
        [SerializeField]
        private string _pickupButton = "";
        [SerializeField]
        private string _throwButton = "";
        [Space]
        [SerializeField]
        private ObjectPickups _pickup;
        [SerializeField]
        private ObjectsThrow _throw;
        [SerializeField]
        private ItemHolder _holder;

        private IItemsInventory _inventory;

        public bool Enabled { get; set; }

        [Inject]
        public void Construct(IItemsInventory inventory)
        {
            _inventory = inventory;
        }

        private void Update()
        {
            if (Enabled == false)
                return;

            if (Input.GetButtonDown(_pickupButton))
                _pickup.TryPickup();

            if (Input.GetButtonDown(_throwButton))
                _throw.TryThrow();

            if (Input.GetButton(_primaryButton))
                _holder.TryPrimaryAction();

            if (Input.GetButtonDown(_secondaryButton))
                _holder.TrySecondaryAction();

            if (Input.mouseScrollDelta.y != 0f)
                _inventory.MoveSelection(Input.mouseScrollDelta.y > 0);
        }
    }
}