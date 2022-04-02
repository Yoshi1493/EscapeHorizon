using UnityEngine;

[CreateAssetMenu(fileName = "New Float", menuName = "Scriptable Object/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public FloatObject musicVolume;
    public FloatObject soundVolume;
}