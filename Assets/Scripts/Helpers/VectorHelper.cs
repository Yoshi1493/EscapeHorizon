using UnityEngine;

public static class VectorHelper
{
    public static Vector3 RotateVectorBy(this Vector3 vector, float degrees)
    {
        Vector3 v = vector;
        float radians = degrees * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(radians) * v.x) - (Mathf.Sin(radians) * v.y);
        v.y = (Mathf.Sin(radians) * v.x) + (Mathf.Cos(radians) * v.y);

        return vector;
    }

}