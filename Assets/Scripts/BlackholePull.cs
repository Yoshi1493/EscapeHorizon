using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholePull : MonoBehaviour
{
    private Rigidbody2D rb;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    void FollowTargetWithoutRotation(Transform target, float distanceToStop, float speed)
    {
        var direction = Vector3.zero;
        if (Vector3.Distance(transform.position, target.position) > distanceToStop)
        {
            direction = target.position - transform.position;

        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}
