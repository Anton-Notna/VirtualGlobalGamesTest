using Game.Ammo;
using UnityEngine;

namespace Game.Interaction
{
    public class AmmoBox : MonoBehaviour, IPickable
    {
        [SerializeField]
        private AmmoType _type;
        [SerializeField]
        private int _amount = 1;

        public AmmoType Type => _type;

        public int Amount => _amount;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_amount <= 1)
                _amount = 1;
        }
#endif
    }
}