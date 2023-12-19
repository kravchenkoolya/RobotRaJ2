using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerMover),typeof(Health))]
    public class PlayerController : MonoBehaviour
    {
       [SerializeField] private PlayerMover _playerMover;
       [SerializeField] private Animator _animator;
       [SerializeField] private Health _health;
        private PlayerState _currentPlayerStates=PlayerState.Idle;
        private InputManager _inputManager;
        private void Awake()
        {
            _inputManager = new InputManager();
            _inputManager.PC.Jump.started += Jump;
            _inputManager.PC.Move.started += MoveStart;
            _inputManager.PC.Move.canceled += MoveEnd;
            _inputManager.PC.Run.started += StartRun;
            _inputManager.PC.Run.canceled += EndRun;
           
            _inputManager.Enable();
         
        }
        private void Jump(InputAction.CallbackContext obj)
        {
      
            switch (_currentPlayerStates)
            {
                case PlayerState.Idle:
                    _animator.SetTrigger("Jump");
                    _playerMover.Jump();
                    _currentPlayerStates = PlayerState.Jump;
                    break;
                case PlayerState.Walk:
                    _animator.SetTrigger("Jump");
                    _playerMover.Jump();
                    _currentPlayerStates = PlayerState.Jump;
                    break;
                case PlayerState.Run:   
                    _animator.SetTrigger("Jump");
                    _playerMover.Jump();
                    _currentPlayerStates = PlayerState.Jump;
                    break;
            }
        }

        private void StartRun(InputAction.CallbackContext obj)
        {
            if(_currentPlayerStates==PlayerState.Walk)
            {
                ToRunning();
            }
            
        }

        private void ToRunning(bool run=true)
        {
            _animator.SetBool("Running", run);
            _playerMover.Run(run);
        }

        private void EndRun(InputAction.CallbackContext obj)
        {
            _animator.SetBool("Running",false);
                _playerMover.Run(false);
        }

        private void MoveEnd(InputAction.CallbackContext obj)
        {
            _playerMover.enabled = false;
            switch (_currentPlayerStates)
            {
                case PlayerState.Walk:
                    ToIdleState();
                    break;
                case PlayerState.Run:
                    ToIdleState();
                    break;
                case PlayerState.Jump:
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dance:
                    break;
            
            }
        }

        private void ToIdleState()
        {
            _currentPlayerStates = PlayerState.Idle;
            _animator.SetTrigger("Idle");
        }
        private void MoveStart(InputAction.CallbackContext obj)
        {
            transform.rotation = Quaternion.identity;
            switch (_currentPlayerStates)
            {
                case PlayerState.Idle:
                    _playerMover.SetDirectional(obj);
                    ToWalkingState();
                    if (_inputManager.PC.Run.IsPressed())
                    {
                        ToRunning();
                    }
                    break;
            }
        }

        private void ToWalkingState()
        {
            _animator.SetTrigger("Walking");
            _currentPlayerStates = PlayerState.Walk;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            { 
                Debug.Log("enter floor");
                if (_currentPlayerStates == PlayerState.Jump)
                {
                    if (_inputManager.PC.Move.IsPressed())
                    {
                        ToWalkingState();
                    }
                    else
                    {
                        ToIdleState();
                    }
                }
            }
        }

 

        private enum PlayerState
        {
            Idle,Walk,Run,Jump,Dying,Dance
        }

    }
}

