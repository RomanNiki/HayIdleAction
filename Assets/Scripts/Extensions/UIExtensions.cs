using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Extensions
{
    public static class UIExtensions
    {
        public static Sequence DoTextAndShake(this TMP_Text text, Transform shakeTransform, string endText,
            float shakeDuration, float shakeStrength)
        {
            var s = DOTween.Sequence();
            s.Append(shakeTransform.DOShakePosition(shakeDuration, shakeStrength));
            text.text = endText;
            return s;
        }

        public static Sequence DoTextAndShake(this TMP_Text text, string endText,
            float shakeDuration, float shakeStrength)
        {
            return text.DoTextAndShake(text.transform, endText, shakeDuration, shakeStrength);
        }
    }
}