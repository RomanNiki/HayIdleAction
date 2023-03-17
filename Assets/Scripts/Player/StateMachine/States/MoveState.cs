using Controls;
using UnityEngine;

namespace Player.StateMachine.States
{
    public class MoveState : State
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _gravity = -9.8f;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private InputAdapter _inputAdapter;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            var moveDirection = new Vector3(_inputAdapter.Direction.x, _gravity, _inputAdapter.Direction.y) * _speed;
            
            _characterController.Move(moveDirection);
            var position = _transform.position;
            var lookDirection = moveDirection + position;
            transform.LookAt(new Vector3(lookDirection.x, position.y, lookDirection.z));
        }
    }
}