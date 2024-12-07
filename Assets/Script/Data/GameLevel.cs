using System.Collections.Generic;

public enum GameLevel
{
    Level1,
    Level2,
    Level3,
    None
}

public static class GameLevelMap
{
    public static readonly Dictionary<GameLevel, string> LevelToSceneName = new Dictionary<GameLevel, string>
    {
        { GameLevel.Level1, "GameLevel1" },
        { GameLevel.Level2, "GameLevel2" },
        { GameLevel.Level3, "GameLevel3" }
    };
}
