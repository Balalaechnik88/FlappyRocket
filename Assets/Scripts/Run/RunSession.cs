using System;
using UnityEngine;

public class RunSession : MonoBehaviour
{
    public event Action Finished;

    public bool IsFinished { get; private set; }

    public void Finish()
    {
        if (IsFinished)
            return;

        IsFinished = true;
        Time.timeScale = 0f;
        Finished?.Invoke();
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}