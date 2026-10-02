using UnityEngine;
using UnityEngine.Splines;

public class SplineStop : MonoBehaviour
{
    [Header("Spline")]
    public SplineContainer spline;

    [Range(0f, 1f)]
    public float splinePosition;
}