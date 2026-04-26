using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;

    private Vector2 _travelDirection;
    private float _travelSpeed;
    private float _remainingLifeTime;
    private ProjectileSide _projectileSide;
    private Collider2D _ignoredCollider;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _collider.isTrigger = true;
        _rigidbody.gravityScale = 0f;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Launch(
        Vector2 travelDirection,
        float travelSpeed,
        ProjectileSide projectileSide,
        Collider2D ignoredCollider)
    {
        _travelDirection = travelDirection;
        _travelSpeed = travelSpeed;
        _projectileSide = projectileSide;
        _remainingLifeTime = _lifeTime;
        _ignoredCollider = ignoredCollider;

        if (_ignoredCollider != null)
            Physics2D.IgnoreCollision(_collider, _ignoredCollider, true);

        RotateAlongDirection();
    }

    private void Update()
    {
        Move();
        UpdateLifetime();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == _ignoredCollider)
            return;

        if (other.GetComponent<Projectile>() != null)
            return;

        IProjectileImpactReceiver impactReceiver = other.GetComponent<IProjectileImpactReceiver>();

        if (impactReceiver != null && impactReceiver.DamagingSide == _projectileSide)
        {
            impactReceiver.ReceiveProjectileImpact();
            Destroy(gameObject);
            return;
        }

        if (other.isTrigger)
            return;

        Destroy(gameObject);
    }

    private void Move()
    {
        transform.position += (Vector3)(_travelDirection * _travelSpeed * Time.deltaTime);
    }

    private void UpdateLifetime()
    {
        _remainingLifeTime -= Time.deltaTime;

        if (_remainingLifeTime <= 0f)
            Destroy(gameObject);
    }

    private void RotateAlongDirection()
    {
        float angle = Mathf.Atan2(_travelDirection.y, _travelDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}