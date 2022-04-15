using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour {
    public static GameController Instance;
    public string currentLevel;
    public Vector2 lastSavePoint;

    public List<string> selectableLevel;
    // public HashSet<string> selectableLevel;

    private void Awake() {
        Instance = this;
        Load();
    }
    
    public void SetLevelClear(string sceneName) {
        if (!selectableLevel.Contains(sceneName)) {
            selectableLevel.Add(sceneName);
        }
        Save();
    }

    public void SetLastSavePoint(Vector2 pos) {
        lastSavePoint = pos;
        Save();
    }

    public void SetCurrentScene(string sceneName) {
        currentLevel = sceneName;
        Save();
    }

    public String GetCurrentLevel() {
        return currentLevel;
    }

    private void Save() {
        PlayerPrefs.SetString("GameData", JsonUtility.ToJson(Instance));
    }

    private void Load() {
        if (!PlayerPrefs.HasKey("GameData")) {
            // Init
            currentLevel = "Scene_0";
            selectableLevel = new List<string>();
        } else {
            JsonUtility.FromJsonOverwrite(
                PlayerPrefs.GetString("GameData"),
                Instance
            );
        }
    }

    private void Update() { }
}