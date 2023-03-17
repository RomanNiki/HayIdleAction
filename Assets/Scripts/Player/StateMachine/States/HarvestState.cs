using UnityEngine;

namespace Player.StateMachine.States
{
    public class HarvestState : State
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Player _player;
        [Range(1,10)][SerializeField] private float _harvestSpeed = 5f;
        [SerializeField] private GameObject _sickleParent;
        
        private bool _canExtract;

        private void OnEnable()
        {
            _sickleParent.SetActive(true);
            _canExtract = true;
        }

        private void OnDisable()
        {
            _sickleParent.SetActive(false);
        }

        private void Update()
        {
            if (_player.HasCrops == false ||  _canExtract == false) return;
            if (_player.GetLastCrop.IsExtractable == false || enabled == false)
                return;
            _canExtract = false;
            _playerAnimator.PlayExtractCrop(_harvestSpeed);
        }
        

        public void ExtractHarvest()
        {
            var crop = _player.GetLastCrop;
            if (crop != null)
            {
                crop.Extract();
            }
            
            _canExtract = true;
        }
    }
}