using UnityEngine;
using TMPro;

public class GameOverMenu : Menu
{
    [SerializeField] TextMeshProUGUI timeSurvivedText;
    const string MonospaceTag = "<mspace=25>";

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<Player>().GameOverAction += OnGameOver;
    }

    void OnGameOver()
    {
        Open();

        string timeSurvived = FindObjectOfType<TimeCounter>().timeText.text.Substring(MonospaceTag.Length);
        timeSurvivedText.text = $"Time survived:\n{timeSurvived}";
    }
}