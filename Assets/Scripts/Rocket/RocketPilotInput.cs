using UnityEngine;

public class RocketPilotInput : MonoBehaviour
{
    [SerializeField] private RunSession _runSession;
    [SerializeField] private KeyCode _flapKey = KeyCode.Space;
    [SerializeField] private KeyCode _shootKey = KeyCode.F;
    [SerializeField] private RocketFlight _rocketFlight;
    [SerializeField] private RocketGun _rocketGun;

    private void Update()
    {
        if (_runSession != null && _runSession.IsFinished)
            return;

        if (Input.GetKeyDown(_flapKey))
            _rocketFlight?.Flap();

        if (Input.GetKeyDown(_shootKey))
            _rocketGun?.FireInLookDirection();
    }
}