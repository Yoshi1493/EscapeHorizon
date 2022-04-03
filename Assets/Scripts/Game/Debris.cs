using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class Debris : Actor
{
    float moveSpeed;
    const float MinInitialSpeed = 1f;
    const float MaxInitialSpeed = 3f;
    const float DispelSpeed = 10f;

    IEnumerator rotateCoroutine;
    float RotationSpeed { get => Random.Range(-30f, 30f); }

    void OnEnable()
    {
        this.gameObject.layer = 8;
        Vacuum vacuum = FindObjectOfType<Vacuum>();
        vacuum.VacuumAction += OnPlayerVacuum;
        vacuum.DispelAction += OnPlayerDispel;

        moveDirection = transform.up;
        moveSpeed = Random.Range(MinInitialSpeed, MaxInitialSpeed);

        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }

        rotateCoroutine = Rotate(RotationSpeed);
        StartCoroutine(rotateCoroutine);
    }

    IEnumerator Rotate(float rotationSpeed)
    {
        while (enabled)
        {
            spriteRenderer.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            yield return EndOfFrame;
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);
    }

    void OnPlayerVacuum(Debris debris)
    {
        debris.moveSpeed = 0f;
    }

    void OnPlayerDispel(Debris debris, Vector3 resultingDirection)
    {
        debris.moveDirection = resultingDirection;
        debris.moveSpeed = DispelSpeed;
    }
}