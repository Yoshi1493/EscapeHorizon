using System;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public event Action<bool> GamePauseAction;
    bool isPaused;

    void Awake()
    {
        GamePauseAction += OnGamePaused;

        FindObjectOfType<BlackHole>().PlayerEatenAction += OnPlayerEaten;
    }

    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            SetGamePaused(!isPaused);
        }
    }

    public void SetGamePaused(bool pauseState)
    {
        GamePauseAction?.Invoke(pauseState);
    }

    void OnGamePaused(bool pauseState)
    {
        isPaused = pauseState;
    }

    void OnPlayerEaten()
    {
        enabled = false;
    }
}
