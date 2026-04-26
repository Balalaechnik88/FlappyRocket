using UnityEngine;

public class RocketViewportExit : MonoBehaviour
{
    private const float BottomViewportLimit = -0.05f;
    private const float TopViewportLimit = 1.05f;

    [SerializeField] private RocketHull _rocketHull;
    [SerializeField] private Camera _targetCamera;

    private void Awake()
    {
        if (_targetCamera == null)
            _targetCamera = Camera.main;
    }

    private void Update()
    {
        if (_rocketHull == null || _targetCamera == null)
            return;

        Vector3 viewportPosition = _targetCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.y < BottomViewportLimit || viewportPosition.y > TopViewportLimit)
            _rocketHull.BreakHull();
    }
}