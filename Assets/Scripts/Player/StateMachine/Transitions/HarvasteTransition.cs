using Controls;
using UnityEngine;

namespace Player.StateMachine.Transitions
{
    public class HarvasteTransition : Transition
    {
        [SerializeField] private InputAdapter _inputAdapter;
        [SerializeField] private Player _player;

        private void Update()
        {
            if (_inputAdapter.Direction.sqrMagnitude > 0f) return;
            if (_player.HasCrops) NeedTransit = true;
        }
    }
}