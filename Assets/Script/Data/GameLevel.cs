using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Game Level Variable")]
public class GameLevel : ScriptableObject
{
    public string level;
    public bool isUnlocked;
}
