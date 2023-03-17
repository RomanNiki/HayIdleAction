using UnityEngine;

namespace Controls
{
    public abstract class InputAdapter : MonoBehaviour
    {
        public Vector2 Direction { get; protected set; }
    }
}