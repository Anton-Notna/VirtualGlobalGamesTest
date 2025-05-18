using UnityEngine;

namespace Game.Items
{
    public class ItemGfx : MonoBehaviour
    {
        [SerializeField]
        private Collider _collider;
        [SerializeField]
        private Vector3 _holdOffset;

        public bool Collisions
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        public Vector3 HoldOffset => -_holdOffset;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.TransformPoint(_holdOffset), 0.05f);
        }
#endif
    }
}