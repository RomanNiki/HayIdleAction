using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Extensions;
using Garden;
using UnityEngine;

namespace Player.Inventory
{
    public class PlayerBlockCollector : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private Transform _backPackTransform;
        [SerializeField] private Transform _parentTransform;
        [SerializeField] private float _scaleDuration = 0.3f;
        [SerializeField] private float _jumpDuration = 0.2f;
        [SerializeField] private float _jumpPower = 1.5f;
        [SerializeField] private float _pullSpeedFactor = 1.5f;
        [SerializeField] private float _pullMinSqrMagnitude = 0.5f;
        [SerializeField] private Vector3 _blockInInventoryScale = new Vector3(0.1f, 0.1f, 0.1f);
        private int _collectingItems;
        private readonly List<Vector3> _backpackItemPositionOffset = new List<Vector3>();

        private void Start()
        {
            CalculateBackpackPositions();
        }

        private void CalculateBackpackPositions()
        {
            var position = _backPackTransform.position;
            var localScale = _backPackTransform.lossyScale;
            var startPosition = new Vector3(-localScale.x / 2,
                position.y, -localScale.z / 2);
            var endPosition = new Vector3(localScale.x / 2,
                position.y, localScale.z / 2);
            var y = 0.1f;
            while (_backpackItemPositionOffset.Count < _playerInventory.MaxCapacity)
            {
                for (var i = startPosition.x; i < endPosition.x; i += _blockInInventoryScale.x)
                {
                    for (var j = startPosition.z; j < endPosition.z; j += _blockInInventoryScale.z)
                    {
                        _backpackItemPositionOffset.Add(new Vector3(i, y, j));
                    }
                }

                y += _blockInInventoryScale.y;
            }
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CropBlock block))
            {
                await TryCollectBlock(block);
            }
        }

        private async Task TryCollectBlock(CropBlock block)
        {
            if (_playerInventory.CanAdd(_collectingItems))
            {
                _collectingItems++;
                block.DisableCollider();
                await PullObject(block, _parentTransform, _blockInInventoryScale, GetCurrentPosition());
                _playerInventory.AddBlock(block);
                _collectingItems--;
            }
        }

        private async Task PullObject(Component block, Transform targetTransform, Vector3 targetScale,
            Vector3 targetLocalPosition)
        {
            var blockTransform = block.transform;
            blockTransform.DOKill();
            await block.DoJumpScaleItemTo(targetTransform.position + targetLocalPosition, targetScale, _scaleDuration,
                _jumpPower, _jumpDuration).AsyncWaitForCompletion();
            await blockTransform.PullObject(_parentTransform, _pullSpeedFactor, _pullMinSqrMagnitude);
            blockTransform.parent = _parentTransform;
            blockTransform.rotation = _parentTransform.rotation;
            blockTransform.localScale = targetScale;
            blockTransform.localPosition = targetLocalPosition;
        }


        private Vector3 GetCurrentPosition()
        {
            return _backpackItemPositionOffset[_playerInventory.BlocksCount + _collectingItems - 1];
        }
    }
}