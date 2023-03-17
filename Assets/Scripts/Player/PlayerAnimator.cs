using Controls;
using Extensions;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animatorController;
        [SerializeField] private InputAdapter _inputAdapter;

        private void Update()
        {
            _animatorController.SetFloat(PlayerAnimatorController.Parameters.Acceleration,
                _inputAdapter.Direction.sqrMagnitude);
        }

        public void PlayExtractCrop(float harvestSpeed = 1f)
        {
           _animatorController.SetFloat(PlayerAnimatorController.Parameters.HarvestSpeed, harvestSpeed);
           _animatorController.SetTrigger(PlayerAnimatorController.Parameters.Harvest);
        }
    }
}