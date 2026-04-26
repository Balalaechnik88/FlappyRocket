using UnityEngine;

public class RocketHull : MonoBehaviour, IProjectileImpactReceiver
{
    [SerializeField] private RunSession _runSession;

    private bool _isBroken;

    public ProjectileSide DamagingSide => ProjectileSide.Enemy;

    public void ReceiveProjectileImpact()
    {
        BreakHull();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BreakHull();
    }

    public void BreakHull()
    {
        if (_isBroken)
            return;

        _isBroken = true;
        gameObject.SetActive(false);

        if (_runSession != null)
            _runSession.Finish();
    }
}