using Player.StateMachine.States;
using UnityEngine;

namespace Player.StateMachine.Transitions
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;
        public State TargetState => _targetState;
        public bool NeedTransit { get; protected set; }

        protected virtual void OnEnable()
        {
            NeedTransit = false;
        }
    }
}