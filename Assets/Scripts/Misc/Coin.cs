using Interfaces;
using UnityEngine;

namespace Misc
{
    public class Coin : MonoBehaviour, IPoolObject<Coin>
    {
        [field: SerializeField] public int Coast { get; private set; } = 1;
        private IFactory<Coin> _factory;
        
        public void Despawn()
        {
            _factory.Despawn(this);
        }

        public void Init(IFactory<Coin> factory)
        {
            _factory = factory;
        }
    }
}