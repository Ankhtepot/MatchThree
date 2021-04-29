using Enums;
using UnityEngine;

namespace Utils
{
    public static class MoveInterpolation
    {
        public static float Get(EInterpType interpType, float t) => interpType switch
        {
            EInterpType.Linear => t,
            EInterpType.EaseIn => 1 - Mathf.Cos(t * Mathf.PI * 0.5f),
            EInterpType.EaseOut => Mathf.Sin(t * Mathf.PI * 0.5f),
            EInterpType.SmoothStep => t * t * (3 - 2 * t),
            EInterpType.SmootherStep => t * t * t * (t * (t * 6 - 15) + 10),
            _ => t
        };
    }
}