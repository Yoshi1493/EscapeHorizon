using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class BlackHole : Actor
{
    const float DebrisScaleFactor = 0.5f;
    const float ConstantScaleFactor = 0.05f;

    ParticleSystem visualEffect;
    [SerializeField] AnimationCurve scaleInterpolationCurve;

    IEnumerator expandCoroutine;

    protected override void Awake()
    {
        base.Awake();
        visualEffect = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (expandCoroutine == null)
        {
            Vector3 newScale = transform.localScale;
            newScale += ConstantScaleFactor * Time.deltaTime * Vector3.one;
            newScale.z = 1f;

            transform.localScale = newScale;
            visualEffect.transform.localScale = newScale;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Debris debris))
        {
            if (expandCoroutine != null)
            {
                StopCoroutine(expandCoroutine);
            }

            expandCoroutine = Expand(DebrisScaleFactor);
            StartCoroutine(expandCoroutine);
        }
    }

    IEnumerator Expand(float amount)
    {
        float currentLerpTime = 0f;
        float totalLerpTime = amount;

        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale;
        endScale.x += amount;
        endScale.y += amount;

        while (transform.localScale != endScale)
        {
            float lerpProgress = currentLerpTime / totalLerpTime;
            Vector3 newScale = Vector3.Lerp(startScale, endScale, scaleInterpolationCurve.Evaluate(lerpProgress));

            transform.localScale = newScale;
            visualEffect.transform.localScale = newScale;

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        transform.localScale = endScale;
        expandCoroutine = null;
    }
}
