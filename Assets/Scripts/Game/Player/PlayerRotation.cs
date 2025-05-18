using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;
        [SerializeField]
        private Transform _head;
        [SerializeField, Range(0f, 90f)]
        private float _maxPitch = 89f;
        [SerializeField]
        private float _rotationSpeed = 1f;

        private float _horizontal;
        private float _vertical;

        public void UpdateRotation(Vector2 rawRotation)
        {
            rawRotation *= _rotationSpeed;
            _horizontal += rawRotation.x;
            _vertical += rawRotation.y;
            _vertical = Mathf.Clamp(_vertical, -_maxPitch, _maxPitch);

            _root.transform.localRotation = Quaternion.Euler(Vector3.up * _horizontal);
            _head.transform.localRotation = Quaternion.Euler(Vector3.right * _vertical);
        }
    }
}