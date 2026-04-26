using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShooterDroneFlight : MonoBehaviour
{
    [SerializeField] private RunSession _runSession;
    [SerializeField] private float _moveSpeed = 2.5f;
    [SerializeField] private float _leftDespawnBoundary = -12f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        if (_runSession != null && _runSession.IsFinished)
            return;

        MoveLeft();
        RemoveBehindBoundary();
    }

    private void MoveLeft()
    {
        Vector2 nextPosition = _rigidbody.position + Vector2.left * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(nextPosition);
    }

    private void RemoveBehindBoundary()
    {
        if (transform.position.x < _leftDespawnBoundary)
            Destroy(gameObject);
    }
}