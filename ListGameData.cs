using System.Collections.Generic;
using UnityEngine;

public class GameLevel
{
    public string level;
    public bool isUnlocked;
}

[CreateAssetMenu(menuName = "ScriptableObject/Game Data Variable")]
public class ListGameData : ScriptableObject
{
    [SerializeField]
    public List<GameLevel> gameData = new List<GameLevel>();
}
