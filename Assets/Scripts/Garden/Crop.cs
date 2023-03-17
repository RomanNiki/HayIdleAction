using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Garden
{
    public class Crop : MonoBehaviour
    {
        [field: SerializeField, Range(1, 10)] public int StagesCount { get; private set; }
        [SerializeField] private GameObject _cropPrefab;
        [SerializeField] private Vector3 _spawnOffset;
        [SerializeField] private CropBlockSpawner _cropBlockSpawner;
        [SerializeField] private int _blockSpawnCount = 1;
        [SerializeField] private UnityEvent<int> _stageChanged;

        public event UnityAction<int> StageChanged
        {
            add => _stageChanged.AddListener(value);
            remove => _stageChanged.RemoveListener(value);
        }

        private List<GameObject> _hulls;
        private int _currentStage;

        private void Start()
        {
            _hulls = SliceExtension.InitSlice(_cropPrefab, transform, _spawnOffset, StagesCount);
        }
        
        public bool IsExtractable => _currentStage < StagesCount;

        public void Grow()
        {
            ChangeVisibleHull(_currentStage, 0);
            _currentStage = 0;
            _hulls[_currentStage].SetActive(true);
        }

        public void Extract()
        {
            if (_currentStage == StagesCount)
                return;
            ChangeVisibleHull(_currentStage, ++_currentStage);
            for (var i = 0; i < _blockSpawnCount; i++)
            {
                _cropBlockSpawner.SpawnCropBlock();
            }
        }

        private void ChangeVisibleHull(int last, int current)
        {
            _hulls[last].SetActive(false);
            _hulls[current].SetActive(true);
            _stageChanged?.Invoke(current);
        }
    }
}