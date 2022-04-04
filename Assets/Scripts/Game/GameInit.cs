using UnityEngine;

public class GameInit : MonoBehaviour
{
    // enable visual effects at startup because they're causing frame drops in editor
    void Start()
    {
        var effects = GetComponentsInChildren<MeshRenderer>(true);
        foreach (var effect in effects)
        {
            effect.enabled = true;
        }
    }
}