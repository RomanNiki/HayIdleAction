using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Money : MonoBehaviour, ICounter<int>
    {
        [SerializeField] private UnityEvent<int> _moneyCountChanged;

        public event UnityAction<int> CountChanged
        {
            add => _moneyCountChanged.AddListener(value);
            remove => _moneyCountChanged.RemoveListener(value);
        }
        public int MoneyCount { get; private set; }

        public void AddMoney(int count)
        {
            if (count <= 0) 
                throw new ArgumentException($"Argument {nameof(count)} cannot be equal or less then zero");
            MoneyCount += count;
            _moneyCountChanged?.Invoke(MoneyCount);
        }
    }
}