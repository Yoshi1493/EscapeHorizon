using UnityEngine;

public class GameInit : MonoBehaviour
{
    void Start()
    {
        // enable visual effects at startup because they're causing frame drops in editor
        var effects = GetComponentsInChildren<MeshRenderer>(true);
        foreach (var effect in effects)
        {
            effect.enabled = true;
        }
    }
}