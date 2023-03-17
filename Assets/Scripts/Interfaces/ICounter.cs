using UnityEngine.Events;

namespace Interfaces
{
    public interface ICounter<T>
    {
        event UnityAction<T> CountChanged;
    }
}