using Interfaces;
using Player;
using UnityEngine;

namespace UI
{
    public class UIMoneyCounter : UICounter<int>
    {
        [SerializeField] private Money _money;
        [SerializeField] private string _moneyCountTextTemplate = "{0}$";
        
        protected override string DoString(int count)
        {
            return string.Format(_moneyCountTextTemplate, count);
        }

        protected override ICounter<int> GetCounter()
        {
            return _money;
        }
    }
}