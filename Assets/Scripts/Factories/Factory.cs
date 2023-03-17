using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Factories
{
    public abstract class Factory<T> : MonoBehaviour, IFactory<T>
        where T :MonoBehaviour, IPoolObject<T>
    {
        [SerializeField] private T _cropBlockPrefab;
        [SerializeField] private int _poolCount;
        [SerializeField] [Range(1, 100)] private int _expandPoolCount;
        [SerializeField] private Transform _defaultParent;
        
        private List<T> _cropBlocks;
        private List<T> _inUseCropBlocks;

        private void Start()
        {
            _cropBlocks = new List<T>(_poolCount);
            _inUseCropBlocks = new List<T>(_poolCount);
            ExpandPool(_poolCount);
        }

        private void ExpandPool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var newItem = Instantiate(_cropBlockPrefab, _defaultParent, true);
                newItem.Init(this);
                newItem.gameObject.SetActive(false);
                _cropBlocks.Add(newItem);
            }
        }

        public T Spawn(Vector3 position, Transform parent = null)
        {
            if (_cropBlocks.Count == 0)
            {
                ExpandPool(_expandPoolCount);
            }

            var spawnedBlock = _cropBlocks[_cropBlocks.Count - 1];
            _cropBlocks.Remove(spawnedBlock);
            _inUseCropBlocks.Add(spawnedBlock);
            var spawnedBlockTransform = spawnedBlock.transform;
            spawnedBlockTransform.SetParent(parent);
            spawnedBlockTransform.position = position;
  
            spawnedBlock.gameObject.SetActive(true);
            return spawnedBlock;
        }

        public void Despawn(T obj)
        {
            obj.gameObject.SetActive(false);
            var transform1 = obj.transform;
            transform1.position = Vector3.zero;
            transform1.localScale = _cropBlockPrefab.transform.localScale;
            _inUseCropBlocks.Remove(obj);
            obj.transform.SetParent(_defaultParent);
            _cropBlocks.Add(obj);
        }
    }
}