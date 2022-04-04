using System;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI timeText;
    const string TimeFormat = "m':'ss'.'fff";
    const string MonospaceTag = "<mspace=25>";

    float unpausedTime;

    void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
        FindObjectOfType<BlackHole>().PlayerEatenAction += OnPlayerEaten;
    }

    void Update()
    {
        unpausedTime += Time.deltaTime;
        timeText.text = $"{MonospaceTag}{TimeSpan.FromSeconds(unpausedTime).ToString(TimeFormat)}";
    }

    void OnGamePaused(bool pauseState)
    {
        enabled = !pauseState;
    }

    void OnPlayerEaten()
    {
        OnGamePaused(true);
    }
}