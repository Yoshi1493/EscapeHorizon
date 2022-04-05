using UnityEngine;

public static class VectorHelper
{
    public static Vector3 RotateVectorBy(this Vector3 vector, float degrees)
    {
        Vector3 v = vector;
        float radians = degrees * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(radians) * v.x) - (Mathf.Sin(radians) * v.y);
        v.y = (Mathf.Sin(radians) * v.x) + (Mathf.Cos(radians) * v.y);

        return v;
    }

    public static Vector3 GetRandomSpawnPosition(Vector3 minSpawnRange, Vector3 maxSpawnRange)
    {
        Vector3 v = Random.value > 0.5f ? minSpawnRange : maxSpawnRange;        

        if (Random.value > 0.5f)
        {
            v.x = Random.Range(minSpawnRange.x, maxSpawnRange.x);
        }
        else
        {
            v.y = Random.Range(minSpawnRange.y, maxSpawnRange.y);
        }

        return v;
    }
}