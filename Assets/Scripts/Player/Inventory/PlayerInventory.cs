using System.Collections.Generic;
using Garden;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Inventory
{
    public class PlayerInventory : MonoBehaviour, ICounter<int>
    {
        [SerializeField] private UnityEvent<int> _blocksCountChanged;
        [field:SerializeField]  public int MaxCapacity { get; private set; }
        
        public event UnityAction<int> CountChanged
        {
            add => _blocksCountChanged.AddListener(value);
            remove => _blocksCountChanged.RemoveListener(value);
        }

        private readonly Stack<CropBlock> _blocks = new Stack<CropBlock>();
        public bool CanAdd(int countToAdd) => _blocks.Count + countToAdd < MaxCapacity;
        public bool HasBlocks => _blocks.Count > 0;
      
        public int BlocksCount => _blocks.Count;
        
        private void Start()
        {
            _blocksCountChanged?.Invoke(_blocks.Count);
        }
        
        public void AddBlock(CropBlock block)
        {
            _blocks.Push(block);
            _blocksCountChanged?.Invoke(_blocks.Count);
        }

        public CropBlock TryTakeLastBlock()
        {
            if (HasBlocks == false) return null;
            var block = _blocks.Pop();
            _blocksCountChanged?.Invoke(_blocks.Count);
            return block;
        }
    }
}