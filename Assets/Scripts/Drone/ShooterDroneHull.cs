using UnityEngine;

public class ShooterDroneHull : MonoBehaviour, IProjectileImpactReceiver
{
    private bool _isDestroyed;

    public ProjectileSide DamagingSide => ProjectileSide.Player;

    public void ReceiveProjectileImpact()
    {
        if (_isDestroyed)
            return;

        _isDestroyed = true;
        Destroy(gameObject);
    }
}