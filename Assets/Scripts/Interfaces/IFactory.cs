using UnityEngine;

namespace Interfaces
{
    public interface IFactory<T>
    {
        T Spawn(Vector3 position, Transform parent = null);
        void Despawn(T obj);
    }
}