using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionsMenu : Menu
{

    protected override void Awake()
    {
        base.Awake();
        InitSettings();

        PauseHandler pauseHandler = FindObjectOfType<PauseHandler>();
        if (pauseHandler != null)
        {
            pauseHandler.GamePauseAction += OnGamePaused;
        }
    }

    void InitSettings()
    {
    }


    void OnGamePaused(bool pauseState)
    {
        if (!pauseState)
        {
            Close();
        }
    }
}