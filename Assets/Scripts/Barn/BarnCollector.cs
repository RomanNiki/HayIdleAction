using System;
using System.Threading.Tasks;
using DG.Tweening;
using Extensions;
using Player;
using Player.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Barn
{
    public class BarnCollector : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Money, int> _itemCollected;
        [SerializeField] private float _takeItemCoolDown;
        [SerializeField] private float _scaleDuration = 0.3f;
        [SerializeField] private float _jumpDuration = 0.2f;
        [SerializeField] private float _jumpPower = 1.5f;
        private bool _playerInZone;

        public event UnityAction<Money, int> ItemCollected
        {
            add => _itemCollected.AddListener(value);
            remove => _itemCollected.RemoveListener(value);
        }
        
        private async void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInventory playerInventory) == false) return;
            _playerInZone = true;
            var money = other.GetComponent<Money>();
            await CollectBlocks(playerInventory, money);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerInventory _))
            {
                _playerInZone = false;
            }
        }

        private async Task CollectBlocks(PlayerInventory playerInventory, Money money)
        {
            while (_playerInZone)
            {
                if (playerInventory.HasBlocks == false)
                {
                    await Task.Yield();
                    continue;
                }
                var block = playerInventory.TryTakeLastBlock();
                var blockTransform = block.transform;
                blockTransform.SetParent(null);
                block.DoJumpScaleItemTo(transform.position, blockTransform.localScale, _scaleDuration, _jumpPower,
                    _jumpDuration).OnComplete(() =>
                {
                    _itemCollected?.Invoke(money, block.Coast);
                    block.Despawn();
                });
                
                await Task.Delay(TimeSpan.FromSeconds(_takeItemCoolDown));
            }
        }
    }
}