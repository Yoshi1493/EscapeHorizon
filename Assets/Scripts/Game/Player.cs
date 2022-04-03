using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class Player : MonoBehaviour
{
    public event Action GameOverAction;
    [SerializeField] AnimationCurve scaleInterpolationCurve;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.TryGetComponent(out BlackHole _))
        {
            StartCoroutine(ScaleToZero());
        }
    }

    public IEnumerator ScaleToZero()
    {
        // disable movement input
        GetComponent<PlayerMovement>().enabled = false;

        float currentLerpTime = 0f;
        float totalLerpTime = 2f;

        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;

        // lerp position and scale to (0, 0, 0)
        while (transform.localScale != Vector3.zero)
        {
            float lerpProgress = currentLerpTime / totalLerpTime;
            float actualProgress = scaleInterpolationCurve.Evaluate(lerpProgress);

            transform.position = Vector3.Lerp(startPos, Vector3.zero, actualProgress);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, actualProgress);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        // call gameover events
        GameOverAction?.Invoke();
    }
}