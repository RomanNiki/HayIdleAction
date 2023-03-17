using UnityEngine;

namespace Player.StateMachine.Transitions
{
    public class CantHarvasteTransition : Transition
    {
        [SerializeField] private Player _player;
        
        private void Update()
        {
            if (_player.HasCrops == false) NeedTransit = true;
        }
    }
}