using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static CoroutineHelper;

public class BackgroundController : MonoBehaviour
{
    Image blackBackground;
    IEnumerator fadeCoroutine;

    void Awake()
    {
        blackBackground = GetComponentInChildren<Image>();
    }

    void Start()
    {
        fadeCoroutine = Fade(1f, 0f);
        StartCoroutine(fadeCoroutine);
    }

    public IEnumerator LoadSceneAfterDelay(int sceneIndex)
    {
        blackBackground.raycastTarget = true;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = Fade(0f, 1f);
        yield return fadeCoroutine;

        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float currentLerpTime = 0f;
        float totalLerpTime = 1f;

        Color c = blackBackground.color;
        c.a = startAlpha;
        blackBackground.color = c;

        while (blackBackground.color.a != endAlpha)
        {
            float lerpProgress = currentLerpTime / totalLerpTime;

            c.a = Mathf.Lerp(startAlpha, endAlpha, lerpProgress);
            blackBackground.color = c;

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        c.a = endAlpha;
        blackBackground.color = c;
    }
}