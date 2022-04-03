public class MainMenuConfirm : Menu
{
    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
    }

    void OnGamePaused(bool pauseState)
    {
        if (!pauseState)
        {
            Close();
        }
    }
}