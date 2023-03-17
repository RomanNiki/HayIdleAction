using Interfaces;
using UnityEngine;

namespace Garden
{
    public class CropBlock : MonoBehaviour, IPoolObject<CropBlock>
    {
        [SerializeField] private Collider _collider;
        [field: SerializeField] public int Coast { get; private set; }
        private IFactory<CropBlock> _factory;

        public void Init(IFactory<CropBlock> factory)
        {
            _factory = factory;
        }
        
        private void OnEnable()
        {
            _collider.enabled = true;
        }

        public void DisableCollider()
        {
            _collider.enabled = false;
        }

        public void Despawn()
        {
            _factory.Despawn(this);
        }
    }
}