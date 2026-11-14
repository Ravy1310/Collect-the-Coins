using UnityEngine;

/// <summary>
/// Kamera mengikuti pemain dari jarak dan sudut yang dapat diatur di Inspector.
/// FR-06, NFR-03
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _target;

    [Header("Follow Settings")]
    [SerializeField] private float _distance = 6f;
    [SerializeField] private float _height = 3f;
    [SerializeField] private float _smoothSpeed = 10f;

    [Header("Look At Offset")]
    [SerializeField] private float _lookAtHeightOffset = 1f;

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.position
            - _target.forward * _distance
            + Vector3.up * _height;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            _smoothSpeed * Time.deltaTime
        );

        Vector3 lookAtPoint = _target.position + Vector3.up * _lookAtHeightOffset;
        transform.LookAt(lookAtPoint);
    }

    /// <summary>
    /// Ditetapkan oleh GameManager atau setup scene agar kamera tahu siapa targetnya.
    /// </summary>
    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
