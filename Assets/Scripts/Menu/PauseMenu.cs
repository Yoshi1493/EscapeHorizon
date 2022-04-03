public class PauseMenu : Menu
{
    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();

        pauseHandler = FindObjectOfType<PauseHandler>();
        pauseHandler.GamePauseAction += OnGamePaused;
    }

    void OnGamePaused(bool pauseState)
    {
        if (pauseState)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void OnSelectResume()
    {
        pauseHandler.SetGamePaused(false);
    }
}