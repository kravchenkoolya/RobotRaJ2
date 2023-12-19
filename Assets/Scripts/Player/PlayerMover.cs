using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _normalSpeed;
        [SerializeField] private float _boost;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Transform _bodyTransform;
        private float _speed;
        private InputAction.CallbackContext _moveDirection;

        private void Awake() => _speed = _normalSpeed;

        public void SetDirectional(InputAction.CallbackContext readValue)
        {
            enabled = true;
            _moveDirection = readValue;
        }

        private void Update() => _rigidbody.MovePosition(transform.position +  CalculatePos() *_speed*Time.deltaTime);

        private Vector3 CalculatePos()
        {
            Debug.Log("X "+_moveDirection.ReadValue<Vector2>().x + " Y "+_moveDirection.ReadValue<Vector2>().y );
            Vector3 addPos=Vector3.zero;
            if (_moveDirection.ReadValue<Vector2>().x > 0)
            {
                addPos = transform.right;
            }
            else if (_moveDirection.ReadValue<Vector2>().x < 0)
            {
                addPos = transform.right*-1;
            }
            if (_moveDirection.ReadValue<Vector2>().y > 0)
            {
                addPos +=Camera.main.transform.forward ;
            }
            else if (_moveDirection.ReadValue<Vector2>().y < 0)
            {
                addPos +=Camera.main.transform.forward *-1;
            }

            return addPos;
        }
        
        public void Run(bool running)
        {
            if (running)
            {
                _speed = _normalSpeed + _boost;
            }
            else
            {
                _speed = _normalSpeed;
            }
        }

        public void Jump() => _rigidbody.AddForce(Vector3.up * _jumpForce);
        
    }
}