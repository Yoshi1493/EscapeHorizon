using System;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    new Transform transform;
    [SerializeField] Transform debrisParent;

    [SerializeField] FloatObject moveSpeed;
    const float OriginalMoveSpeed = 5f;

    const float FieldOfView = 45f;
    const float RangeOfView = 2f;
    const int RayDensity = 9;
    private LayerMask DebrisLayer;

    [SerializeField] ParticleSystem vacuumEffect;
    [SerializeField] ParticleSystem dispelEffect;

    public event Action<Debris> VacuumAction;
    public event Action<Debris, Vector3> DispelAction;

    void Awake()
    {
        transform = GetComponent<Transform>();
        moveSpeed.Value = OriginalMoveSpeed;

        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
        FindObjectOfType<BlackHole>().PlayerEatenAction += OnPlayerEaten;

        DebrisLayer = LayerMask.GetMask("Debris");
    }

    void Update()
    {
        GetLeftMouseButtonInput();
        GetRightMouseButtonInput();
    }

    void GetLeftMouseButtonInput()
    {
        if (debrisParent.childCount == 0 && Input.GetMouseButton(0))
        {
            moveSpeed.Value = 0f;

            if (!vacuumEffect.isPlaying)
            {
                vacuumEffect.Play();
            }

            CheckForCollisions();
        }
        else
        {
            moveSpeed.Value = OriginalMoveSpeed;
        }

        // stop the particle system if it hasn't been stopped already
        if (Input.GetMouseButtonUp(0))
        {
            if (!vacuumEffect.isStopped)
            {
                vacuumEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }

    void CheckForCollisions()
    {
        for (int i = 0; i < RayDensity; i++)
        {
            // find how many degrees to stagger each raycast by
            float raySpread = FieldOfView / (RayDensity - 1);

            // find how many degrees to rotate each raycast by
            float theta = (FieldOfView * -0.5f) + (i * raySpread);

            // get all ray directions based on field of view and raycast rotation amount
            Vector3 direction = transform.up.RotateVectorBy(theta);

            // perform raycast
            var hit = Physics2D.Raycast(transform.position, direction, RangeOfView, DebrisLayer);

            if (hit)
            {
                Transform hitTransform = hit.transform;

                // parent the hit gameobject with the debris parent transform
                hitTransform.parent = debrisParent;

                // invoke action
                VacuumAction?.Invoke(hitTransform.GetComponent<Debris>());

                // play sfx
                AudioManager.Instance.PlaySound("vacuum");

                return;
            }
        }
    }

    void GetRightMouseButtonInput()
    {
        // un-parent all children from debris parent transform
        if (debrisParent.childCount != 0 && Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < debrisParent.childCount; i++)
            {
                DispelAction?.Invoke(debrisParent.GetChild(i).GetComponent<Debris>(), transform.up);
                AudioManager.Instance.PlaySound("dispel");

                debrisParent.GetChild(i).parent = null;

                if (dispelEffect.isPlaying)
                {
                    dispelEffect.Stop();
                }

                dispelEffect.Play();
            }
        }
    }

    void OnGamePaused(bool pauseState)
    {
        enabled = !pauseState;

        if (vacuumEffect.isPlaying)
        {
            vacuumEffect.Stop();
        }

        if (dispelEffect.isPlaying)
        {
            dispelEffect.Stop();
        }
    }

    void OnPlayerEaten()
    {
        OnGamePaused(true);
    }
}