using System.Collections;
using UnityEngine;

public class ShooterDroneGun : MonoBehaviour
{
    [SerializeField] private RunSession _runSession;
    [SerializeField] private ProjectileLauncher _projectileLauncher;
    [SerializeField] private float _shotInterval = 1.4f;

    private Coroutine _attackRoutine;

    private void OnEnable()
    {
        if (_attackRoutine == null)
            _attackRoutine = StartCoroutine(AttackRoutine());
    }

    private void OnDisable()
    {
        if (_attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }
    }

    private IEnumerator AttackRoutine()
    {
        WaitForSeconds shotDelay = new WaitForSeconds(_shotInterval);

        yield return shotDelay;

        while (true)
        {
            if ((_runSession == null || _runSession.IsFinished == false) && _projectileLauncher != null)
                _projectileLauncher.TryFire(Vector2.left);

            yield return shotDelay;
        }
    }
}