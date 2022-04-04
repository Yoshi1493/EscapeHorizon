using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class Debris : Actor
{
    float moveSpeed;
    const float MinInitialSpeed = 1f;
    const float MaxInitialSpeed = 3f;
    const float DispelSpeed = 10f;

    float rotationSpeed;

    [SerializeField] AnimationCurve scaleInterpolationCurve;
    IEnumerator scaleCoroutine;

    Player player;

    protected override void Awake()
    {
        base.Awake();

        Vacuum vacuum = FindObjectOfType<Vacuum>();
        vacuum.VacuumAction += OnPlayerVacuum;
        vacuum.DispelAction += OnPlayerDispel;

        player = FindObjectOfType<Player>();
        player.GameOverAction += OnGameOver;
    }

    void OnEnable()
    {
        gameObject.layer = 8;

        moveDirection = transform.up;
        moveSpeed = Random.Range(MinInitialSpeed, MaxInitialSpeed);
        rotationSpeed = Random.Range(-30f, 30f);
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        if (transform.parent != null)
        {
            moveDirection = transform.parent.position - transform.position;
        }

        moveDirection.Normalize();
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);
    }

    void Rotate()
    {
        spriteRenderer.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void OnPlayerVacuum(Debris debris)
    {
        debris.moveSpeed = 1f;
    }

    void OnPlayerDispel(Debris debris, Vector3 resultingDirection)
    {
        debris.moveDirection = resultingDirection;
        debris.moveSpeed = DispelSpeed;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // check if the other trigger is the black hole
        if (coll.TryGetComponent(out BlackHole _))
        {
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }

            scaleCoroutine = ScaleToZero();
            StartCoroutine(scaleCoroutine);
        }
    }

    IEnumerator ScaleToZero()
    {
        // disable movement input
        moveDirection = Vector3.zero;
        moveSpeed = 0f;

        // disable collider, unparent from everything
        collider.enabled = false;
        transform.parent = null;

        float currentLerpTime = 0f;
        float totalLerpTime = 1f;

        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;

        // lerp position and scale to (0, 0, 0)
        while (transform.localScale != Vector3.zero)
        {
            float lerpProgress = currentLerpTime / totalLerpTime;
            float actualProgress = scaleInterpolationCurve.Evaluate(lerpProgress);

            transform.position = Vector3.Lerp(startPos, Vector3.zero, actualProgress);
            spriteRenderer.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, actualProgress);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;

            // increase rotationSpeed (for the sake of polish)
            rotationSpeed += 10f * Time.deltaTime;
        }

        // return to object pool
        DebrisPool.Instance.ReturnToPool(this);
    }

    protected override void OnGamePaused(bool pauseState)
    {
        base.OnGamePaused(pauseState);
        rigidbody.bodyType = pauseState ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }

    void OnGameOver()
    {
        OnGamePaused(true);
    }
}