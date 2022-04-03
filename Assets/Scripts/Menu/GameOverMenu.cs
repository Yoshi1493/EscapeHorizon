public class GameOverMenu : Menu
{
    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<Player>().GameOverAction += OnGameOver;
    }

    void OnGameOver()
    {
        Open();
    }
}