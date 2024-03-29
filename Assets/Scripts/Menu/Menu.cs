using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class Menu : MonoBehaviour
{
    protected Canvas thisMenu;
    AudioManager audioManager;

    protected virtual void Awake()
    {
        thisMenu = GetComponent<Canvas>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Open()
    {
        thisMenu.enabled = true;

        if (thisMenu.TryGetComponent(out Menu m))
        {
            m.Enable();
        }
    }

    public void Close()
    {
        thisMenu.enabled = false;
        Disable();
    }

    public virtual void Enable()
    {
        if (TryGetComponent(out CanvasGroup cg))
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        enabled = true;
    }

    public virtual void Disable()
    {
        if (TryGetComponent(out CanvasGroup cg))
        {
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }

        enabled = false;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(FindObjectOfType<BackgroundController>().LoadSceneAfterFade(sceneIndex));
    }

    public void PlaySound(string name)
    {
        audioManager.PlaySound(name);
    }
}