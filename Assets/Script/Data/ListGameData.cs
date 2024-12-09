//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObject/Game Data Variable")]
//public class ListGameData : ScriptableObject
//{
//    [SerializeField]
//    public List<GameLevel> gameData = new List<GameLevel>();

//    // Biến tĩnh để theo dõi trạng thái khởi tạo
//    private static bool isInitialized = false;

//    private void Awake()
//    {
//        // Chỉ khởi tạo khi chưa được khởi tạo
//        if (!isInitialized)
//        {
//            InitializeLevels();
//            isInitialized = true;
//        }
//    }

//    public void InitializeLevels()
//    {
//        if (gameData == null)
//        {
//            Debug.LogWarning("Game data null");
//            return;
//        }

//        gameData.Clear();

//        string[] levelNames = { "GameLevel1", "GameLevel2", "GameLevel3", "None" };
//        foreach (var levelName in levelNames)
//        {
//            bool isUnlocked = levelName == "GameLevel1";
//            GameLevel newLevel = ScriptableObject.CreateInstance<GameLevel>();
//            newLevel.level = levelName;
//            newLevel.isUnlocked = isUnlocked;

//            gameData.Add(newLevel);
//        }
//    }
//}
