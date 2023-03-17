using Interfaces;
using Player.Inventory;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIBlocksCounter : UICounter<int>
    {
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private string _counterTextTemplate = "{0} / {1}";

        protected override string DoString(int currentCount)
        {
            return string.Format(_counterTextTemplate, currentCount, _playerInventory.MaxCapacity);
        }

        protected override ICounter<int> GetCounter()
        {
            return _playerInventory;
        }
    }
}