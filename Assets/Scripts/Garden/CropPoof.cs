using UnityEngine;
using UnityEngine.VFX;

namespace Garden
{
    public class CropPoof : MonoBehaviour
    {
        [SerializeField] private Crop _crop;
        [SerializeField] private VisualEffect _visualEffect;
        
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
            _visualEffect.SendEvent(nameof(OnStageChanged));
        }
    }
}