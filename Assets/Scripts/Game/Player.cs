using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class Player : Actor
{
    public event Action GameOverAction;
    [SerializeField] AnimationCurve scaleInterpolationCurve;

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<BlackHole>().PlayerEatenAction += OnPlayerEaten;
    }

    void OnPlayerEaten()
    {
        StartCoroutine(ScaleToZero());
        GetComponent<Vacuum>().enabled = false;
    }

    public IEnumerator ScaleToZero()
    {
        // disable movement input
        GetComponent<PlayerMovement>().enabled = false;

        float currentLerpTime = 0f;
        float totalLerpTime = 2f;

        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;

        // lerp position and scale to (0, 0, 0); lerp colour to black
        while (transform.localScale != Vector3.zero)
        {
            float lerpProgress = currentLerpTime / totalLerpTime;
            float actualProgress = scaleInterpolationCurve.Evaluate(lerpProgress);

            transform.position = Vector3.Lerp(startPos, Vector3.zero, actualProgress);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, actualProgress);
            spriteRenderer.color = Color.Lerp(Color.white, Color.black, actualProgress);
            print(spriteRenderer.color);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        yield return WaitForSeconds(1f);

        // call gameover events
        GameOverAction?.Invoke();
    }
}