using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    private const float MinimumDirectionSqrMagnitude = 0.001f;

    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Collider2D _ownerCollider;
    [SerializeField] private ProjectileSide _projectileSide;
    [SerializeField] private float _projectileSpeed = 12f;
    [SerializeField] private float _shotCooldown = 0.25f;

    private float _nextShotTime;

    public Transform Muzzle => _muzzle;

    private void Awake()
    {
        if (_ownerCollider == null)
            _ownerCollider = GetComponentInParent<Collider2D>();
    }

    public bool TryFire(Vector2 travelDirection)
    {
        if (_projectilePrefab == null || _muzzle == null)
            return false;

        if (travelDirection.sqrMagnitude < MinimumDirectionSqrMagnitude)
            return false;

        if (Time.time < _nextShotTime)
            return false;

        Projectile projectile = Instantiate(_projectilePrefab, _muzzle.position, Quaternion.identity);
        projectile.Launch(travelDirection.normalized, _projectileSpeed, _projectileSide, _ownerCollider);

        _nextShotTime = Time.time + _shotCooldown;
        return true;
    }
}