using System.Collections;
using UnityEngine;

public class ShooterDroneSpawner : MonoBehaviour
{
    [SerializeField] private RunSession _runSession;
    [SerializeField] private GameObject _dronePrefab;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _spawnOffsetFromRightEdge = 2f;
    [SerializeField] private float _verticalPadding = 1f;

    private Camera _targetCamera;
    private Coroutine _spawnRoutine;

    private void Awake()
    {
        _targetCamera = Camera.main;
    }

    private void OnEnable()
    {
        if (_spawnRoutine == null)
            _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds spawnDelay = new WaitForSeconds(_spawnInterval);

        while (true)
        {
            if (_runSession == null || _runSession.IsFinished == false)
                SpawnDrone();

            yield return spawnDelay;
        }
    }

    private void SpawnDrone()
    {
        if (_dronePrefab == null || _targetCamera == null)
            return;

        float cameraHalfHeight = _targetCamera.orthographicSize - _verticalPadding;
        float cameraHalfWidth = cameraHalfHeight * _targetCamera.aspect;

        float spawnPositionX = _targetCamera.transform.position.x + cameraHalfWidth + _spawnOffsetFromRightEdge;
        float minSpawnPositionY = _targetCamera.transform.position.y - cameraHalfHeight;
        float maxSpawnPositionY = _targetCamera.transform.position.y + cameraHalfHeight;
        float spawnPositionY = Random.Range(minSpawnPositionY, maxSpawnPositionY);

        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0f);
        Instantiate(_dronePrefab, spawnPosition, Quaternion.identity);
    }
}