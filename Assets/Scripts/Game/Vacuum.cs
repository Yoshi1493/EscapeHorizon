using UnityEngine;

public class Vacuum : MonoBehaviour
{
    new Transform transform;
    [SerializeField] Transform debrisParent;

    [SerializeField] FloatObject moveSpeed;
    const float OriginalMoveSpeed = 5f;

    const float FieldOfView = 30f;
    const float RangeOfView = 2f;
    const int RayDensity = 5;

    void Awake()
    {
        transform = GetComponent<Transform>();
        moveSpeed.Value = OriginalMoveSpeed;
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
            CheckForCollisions();
        }
        else
        {
            moveSpeed.Value = OriginalMoveSpeed;
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
            var hit = Physics2D.Raycast(transform.position, direction, RangeOfView);

            if (hit)
            {
                // debug
                Debug.DrawRay(transform.position, RangeOfView * direction, Color.green);

                Transform hitTransform = hit.transform;
                print($"found {hitTransform.name}.");

                // parent the hit gameobject with the debris parent transform
                hitTransform.parent = debrisParent;
            }
            else
            {
                // debug
                Debug.DrawRay(transform.position, RangeOfView * direction, Color.red);
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
                debrisParent.GetChild(i).parent = null;
            }
        }
    }

    Vector3 RotateVectorBy(Vector3 vector, float degrees)
    {
        Vector3 v = vector;
        float radians = degrees * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(radians) * v.x) - (Mathf.Sin(radians) * v.y);
        v.y = (Mathf.Sin(radians) * v.x) + (Mathf.Cos(radians) * v.y);

        return v;
    }
}