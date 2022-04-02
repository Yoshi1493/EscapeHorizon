using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public new Transform transform;

    protected Vector3 moveDirection;
    [SerializeField] protected float speed;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
    }
}