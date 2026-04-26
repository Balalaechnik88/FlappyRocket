using UnityEngine;

public class RocketGun : MonoBehaviour
{
    [SerializeField] private ProjectileLauncher _projectileLauncher;

    public void FireInLookDirection()
    {
        if (_projectileLauncher == null || _projectileLauncher.Muzzle == null)
            return;

        _projectileLauncher.TryFire(_projectileLauncher.Muzzle.right);
    }
}