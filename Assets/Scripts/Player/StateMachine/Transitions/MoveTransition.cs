using Controls;
using UnityEngine;

namespace Player.StateMachine.Transitions
{
    public class MoveTransition : Transition
    {
        [SerializeField] private InputAdapter _inputAdapter;

        private void Update()
        {
            if (_inputAdapter.Direction.sqrMagnitude > 0f) NeedTransit = true;
        }
    }
}