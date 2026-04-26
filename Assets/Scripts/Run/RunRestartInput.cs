using UnityEngine;
using UnityEngine.SceneManagement;

public class RunRestartInput : MonoBehaviour
{
    [SerializeField] private RunSession _runSession;
    [SerializeField] private KeyCode _restartKey = KeyCode.R;

    private void Update()
    {
        if (_runSession == null || _runSession.IsFinished == false)
            return;

        if (Input.GetKeyDown(_restartKey))
            RestartScene();
    }

    private void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}