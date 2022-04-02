using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : Actor
{
    void Start()
    {
    }

    void Update()
    {
        if (this.gameObject.transform.parent != null)
        {

            if (this.gameObject.transform.parent.gameObject.name == "Debris")
            {
                this.gameObject.layer = 0;
            }

            else
            {
                this.gameObject.layer = 8;
            }
        }
        else
        {
            this.gameObject.layer = 8;
        }

    }
}
