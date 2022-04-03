using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [HideInInspector] new public Transform transform;
    protected SpriteRenderer spriteRenderer;

    protected Vector3 moveDirection;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}