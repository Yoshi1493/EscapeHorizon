public class MainMenu : Menu
{
    public void OnSelectQuit()
    {
        StartCoroutine(FindObjectOfType<BackgroundController>().LoadSceneAfterFade(-1, fadeDuration: 2f));
    }
}