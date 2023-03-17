using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Extensions
{
    public static class MoveItemExtension
    {
        public static Sequence DoJumpScaleItemTo(this Component block, Vector3 targetPosition,
            Vector3 targetScale,
            float scaleDuration, float jumpPower, float jumpDuration)
        {
            var blockTransform = block.transform;
            var s = DOTween.Sequence();
            s.Append(blockTransform.DOJump(targetPosition,
                jumpPower, 1, jumpDuration));
            s.Append(blockTransform.DOScale(targetScale, scaleDuration));
            return s;
        }

        public static async Task PullObject(this Transform from, Transform to, float pullSpeedFactor, float minSqrMagnitude = 0.1f)
        {
            var diff =  to.position - from.position;
            while (diff.sqrMagnitude > minSqrMagnitude)
            {
                var position = from.position;
                diff =  to.position - position;
                position += diff / diff.magnitude * pullSpeedFactor * Time.deltaTime;
                from.position = position;
                await Task.Yield();
            } 
        }
    }
}