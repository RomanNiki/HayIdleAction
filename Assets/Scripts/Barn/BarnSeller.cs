using System;
using System.Threading.Tasks;
using DG.Tweening;
using Factories;
using Misc;
using Player;
using UnityEngine;

namespace Barn
{
    public class BarnSeller : MonoBehaviour
    {
        [SerializeField] private BarnCollector _barnCollector;
        [SerializeField] private RectTransform _targetUITransform;
        [SerializeField] private Factory<Coin> _coinFactory;
        [SerializeField] private float _timeBetweenSpawn = 0.02f;
        private Camera _camera;
        private Vector3 _targetPosition;
        private Vector3 SpawnPosition => _camera.WorldToScreenPoint(transform.position);
        
        private void Start()
        {
            _camera = Camera.main;
            _targetPosition = _camera.ScreenToWorldPoint(_targetUITransform.position);
        }

        private void OnEnable()
        {
            _barnCollector.ItemCollected += OnItemCollected;
        }

        private void OnDisable()
        {
            _barnCollector.ItemCollected -= OnItemCollected;
        }

        private async void OnItemCollected(Money money, int coast)
        {
            for (var i = 0; i < coast; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(_timeBetweenSpawn));
                var coin = _coinFactory.Spawn(SpawnPosition, _targetUITransform);

                coin.transform.DOLocalMove(_targetPosition, 1.5f)
                    .OnComplete(() =>
                    {
                        money.AddMoney(coin.Coast);
                        coin.Despawn();
                    });
            }
        }
    }
}