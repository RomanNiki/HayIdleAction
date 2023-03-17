namespace Interfaces
{
    public interface IPoolObject<T>
    {
        void Despawn();
        public void Init(IFactory<T> factory);
    }
}