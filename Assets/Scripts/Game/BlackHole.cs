using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private float xScale;
    private float yScale;
    private float scaleIncrement = 0.5f;
    void Start()
    {
        xScale = this.gameObject.transform.localScale.x;
        yScale = this.gameObject.transform.localScale.y;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print("something");
        if (col.gameObject.layer == 8)
        {
            Destroy(col.gameObject);
            xScale += scaleIncrement;
            yScale += scaleIncrement;
            this.gameObject.transform.localScale = new Vector3(xScale, yScale, this.gameObject.transform.localScale.z);
        }
    }

}
