using System.Collections.Generic;
using Player.StateMachine.Transitions;
using UnityEngine;

namespace Player.StateMachine.States
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;
        protected Player Player { get; set; }

        public void Enter(Player player)
        {
            if (enabled == false)
            {
                enabled = true;
                Player = player;
                
                foreach (var transition in _transitions)
                {
                    transition.enabled = true;
                }
            }
        }

        public void Exit()
        {
            if (enabled)
            {
                foreach (var transition in _transitions)
                    transition.enabled = false;
                enabled = false;
            }
        }

        public State GetNext()
        {
            foreach (var transition in _transitions)
            {
                if (transition.NeedTransit)
                {
                    return transition.TargetState;
                }
            }

            return null;
        }
    }
}