using UnityEngine;

[CreateAssetMenu]
public class LightSourceObject : ScriptableObject
{
    public float lifespan;

    [Space]

    public float range;
    public float intensity;

    [Range(1f, 360f)]
    public float angle;
}