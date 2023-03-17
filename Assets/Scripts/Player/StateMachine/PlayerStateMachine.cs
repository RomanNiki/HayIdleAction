using Player.StateMachine.States;
using UnityEngine;

namespace Player.StateMachine
{
    [RequireComponent(typeof(Player))]
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private State _defaultState;
        private Player _player;
        private State _currentState;
        public State CurrentState => _currentState;
      
        private void Start()
        {
            _player = GetComponent<Player>();
            ResetState(_defaultState);
        }

        private void Update()
        {
            if (_currentState == null) return;

            var next = _currentState.GetNext();
            if (next != null)
            {
                Transit(next);
            }
        }

        private void ResetState(State startState)
        {
            _currentState = startState;

            if (_currentState != null)
            {
                _currentState.Enter(_player);
            }
        }

        private void Transit(State nextState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = nextState;

            if (nextState != null)
            {
                _currentState.Enter(_player);
            }
        }
    }
}