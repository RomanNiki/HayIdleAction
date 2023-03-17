using DG.Tweening;
using Factories;
using UnityEngine;

namespace Garden
{
    public class CropBlockSpawner : MonoBehaviour
    {
        [SerializeField] private CropBlockFactory _cropBlockFactory;
        [SerializeField] private Vector2 _jumpRadius = new Vector2(-10F, 10F);
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _jumpDuration;

        public void SpawnCropBlock()
        {
            var cropBlock = _cropBlockFactory.Spawn(transform.position);
            cropBlock.transform
                .DOJump(GetRandomPositionOutsideScreen() + new Vector3(0, cropBlock.transform.localScale.y, 0),
                    _jumpPower, 1, _jumpDuration)
                .SetEase(Ease.Linear);
        }

        private Vector3 GetRandomPositionOutsideScreen()
        {
            return transform.position + new Vector3(Random.Range(_jumpRadius.x, _jumpRadius.y), 0,
                Random.Range(_jumpRadius.x, _jumpRadius.y));
        }
    }
}