using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Interaction
{
    public class ObjectPickups : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _mask;
        [SerializeField]
        private float _raycastDistance;

        private List<IObjectPickup> _pickups = new List<IObjectPickup>();

        [Inject]
        public void Construct(IEnumerable<IObjectPickup> pickups)
        {
            _pickups = new List<IObjectPickup>(pickups);
        }

        public void TryPickup()
        {
            if (_pickups == null)
                return;

            if (Physics.Raycast(transform.position, transform.forward, out var hit, _raycastDistance, _mask) == false)
                return;

            if (hit.collider.TryGetComponent(out IPickable pickable) == false)
                return;

            for (int i = 0; i < _pickups.Count; i++)
            {
                if (_pickups[i].Pickup(pickable))
                {
                    Destroy(hit.collider.gameObject);
                    return;
                }
            }

            Debug.LogWarning($"Cannot process pickable {hit.collider.gameObject.name} typeof {pickable.GetType()}");
        }
    }
}