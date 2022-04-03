using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [HideInInspector] new public Transform transform;
    [HideInInspector] new public Rigidbody2D rigidbody;
    [HideInInspector] new public Collider2D collider;
    protected SpriteRenderer spriteRenderer;

    protected Vector3 moveDirection;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
    }

    protected virtual void OnGamePaused(bool pauseState)
    {
        enabled = !pauseState;
    }
}