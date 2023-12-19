using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 1f;
        [SerializeField] private Rigidbody _rigidbody;

        private void Awake() => Cursor.lockState = CursorLockMode.Locked;

        private void Update()
        {
            Vector3 rotate = new Vector3(0, Mouse.current.delta.x.value * _sensitivity, 0);
            _rigidbody.rotation=Quaternion.Euler(_rigidbody.rotation.eulerAngles-rotate);
            transform.eulerAngles = transform.eulerAngles - rotate;
        }
    }
}