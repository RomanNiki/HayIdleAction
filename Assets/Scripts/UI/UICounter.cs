using DG.Tweening;
using Extensions;
using Interfaces;
using TMPro;
using UnityEngine;

namespace UI
{
    public abstract class UICounter<T> : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private float _shakeDuration = 0.2f;
        [SerializeField] private float _strength = 5f;
        private Vector3 _startPosition;
        
        private void OnEnable()
        {
            GetCounter().CountChanged += OnCountChanged;
        }
        
        private void Start()
        {
            _startPosition = transform.position;
        }

        private void OnDisable()
        {
            GetCounter().CountChanged += OnCountChanged;
        }

        private void OnCountChanged(T count)
        {
            _counterText.DoTextAndShake(transform, DoString(count),
                _shakeDuration, _strength).OnComplete(() => transform.position = _startPosition);
        }

        protected abstract string DoString(T count);

        protected abstract ICounter<T> GetCounter();
    }
}