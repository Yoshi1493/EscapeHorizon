using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static CoroutineHelper;

public class BackgroundController : MonoBehaviour
{
    AudioManager audioManager;

    Image blackBackground;
    IEnumerator fadeCoroutine;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        blackBackground = GetComponentInChildren<Image>();
    }

    void Start()
    {
        fadeCoroutine = FadeBackground(1f, 0f, 1f);
        StartCoroutine(fadeCoroutine);
    }

    public IEnumerator LoadSceneAfterFade(int sceneIndex, float fadeDuration = 1f)
    {
        blackBackground.raycastTarget = true;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        audioManager.FadeMusicVolume(0f, fadeDuration);
        fadeCoroutine = FadeBackground(0f, 1f, fadeDuration);
        yield return fadeCoroutine;

        if (sceneIndex >= 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }

    IEnumerator FadeBackground(float startAlpha, float endAlpha, float fadeDuration)
    {
        if (fadeDuration <= 0) yield break;

        float currentLerpTime = 0f;

        Color c = blackBackground.color;
        c.a = startAlpha;
        blackBackground.color = c;

        while (blackBackground.color.a != endAlpha)
        {
            float lerpProgress = currentLerpTime / fadeDuration;

            c.a = Mathf.Lerp(startAlpha, endAlpha, lerpProgress);
            blackBackground.color = c;

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        c.a = endAlpha;
        blackBackground.color = c;
    }
}