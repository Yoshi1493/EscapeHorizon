using UnityEngine;
using TMPro;

public class GameOverMenu : Menu
{
    [SerializeField] TextMeshProUGUI timeSurvivedText;

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<Player>().GameOverAction += OnGameOver;
    }

    void OnGameOver()
    {
        Open();

        string timeSurvived = FindObjectOfType<TimeCounter>().timeText.text;
        timeSurvivedText.text = $"Time survived:\n{timeSurvived}";
    }
}