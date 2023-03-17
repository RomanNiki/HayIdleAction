using UnityEngine;

namespace Controls
{
    public sealed class JoystickInputAdapter : InputAdapter
    {
        [SerializeField] private Joystick _joystick;

        private void Update()
        {
            Direction = _joystick.Direction;
        }
    }
}