using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Garden
{
    public class CropGrower : MonoBehaviour
    {
        [SerializeField] private Crop _crop;
        [SerializeField] private float _timeToGrow;

        private void OnEnable()
        {
            _crop.StageChanged += OnStageChanged;
        }

        private void OnDisable()
        {
            _crop.StageChanged -= OnStageChanged;
        }

        private void OnStageChanged(int stage)
        {
            TryGrow(stage);
        }

        private async void TryGrow(int stage)
        {
            if (_crop.StagesCount > stage)
            {
                return;
            }

            await Task.Delay(TimeSpan.FromSeconds(_timeToGrow));
            if (_crop != null)
                _crop.Grow();
        }
    }
}