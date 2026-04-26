using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RocketFlight : MonoBehaviour
{
    [SerializeField] private float _flapForce = 6.5f;
    [SerializeField] private float _maxFallSpeed = -10f;
    [SerializeField] private Transform _visualRoot;
    [SerializeField] private float _upTiltAngle = 35f;
    [SerializeField] private float _downTiltAngle = -70f;
    [SerializeField] private float _tiltSmoothing = 10f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateVisualTilt();
    }

    private void FixedUpdate()
    {
        LimitFallSpeed();
    }

    public void Flap()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * _flapForce, ForceMode2D.Impulse);
    }

    private void LimitFallSpeed()
    {
        if (_rigidbody.velocity.y < _maxFallSpeed)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _maxFallSpeed);
    }

    private void UpdateVisualTilt()
    {
        if (_visualRoot == null)
            return;

        float speedRatio = Mathf.InverseLerp(_maxFallSpeed, _flapForce, _rigidbody.velocity.y);
        float targetAngle = Mathf.Lerp(_downTiltAngle, _upTiltAngle, speedRatio);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        _visualRoot.localRotation = Quaternion.Lerp(
            _visualRoot.localRotation,
            targetRotation,
            _tiltSmoothing * Time.deltaTime);
    }
}