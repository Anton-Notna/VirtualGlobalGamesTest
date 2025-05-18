using Game.Items;
using UnityEngine;
using Zenject;

namespace Game.Interaction
{
    public class ObjectsThrow : MonoBehaviour
    {
        [SerializeField]
        private Transform _throwPoint;

        private IFactory<PickableItem> _factory;
        private IItemsInventory _inventory;

        [Inject]
        public void Construct(IFactory<PickableItem> factory, IItemsInventory inventory)
        {
            _factory = factory;
            _inventory = inventory;
        }

        public void TryThrow()
        {
            if (_inventory == null)
                return;

            if (_inventory.Selected.Exists == false)
                return;

            var data = _inventory.ExtractSelected();
            var pickable = _factory.Create();
            pickable.transform.SetLocalPositionAndRotation(_throwPoint.position, _throwPoint.rotation);
            pickable.SetData(data);
        }
    }
}