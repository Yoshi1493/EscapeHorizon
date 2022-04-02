using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    protected Vector3 moveDirection;
    protected new Transform transform;
    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
    }
    protected float speed;

}
