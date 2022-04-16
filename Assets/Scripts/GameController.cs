using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour {
    public static GameController Instance;
    public string currentLevel;
    public int maxJumps;
    public int coins;
    public int keys;
    public Vector2 lastSavePoint;

    public List<string> selectableLevel;
    // public HashSet<string> selectableLevel;

    private void Awake() {
        Instance = this;
        Load();
    }

    public void NewGame() {
        // Init
        currentLevel = "Scene_0";
        selectableLevel = new List<string>();
        maxJumps = 1;
        coins = 0;
        keys = 0;
        Save();
    }

    public void AddCoins(int num) {
        coins += num;
        Save();
    }

    public void AddKeys(int num) {
        keys += num;
        Save();
    }

    public void AddJumps(int num) {
        maxJumps += num;
        Save();
    }
    
    public void SetLevelClear(string sceneName) {
        if (!selectableLevel.Contains(sceneName)) {
            selectableLevel.Add(sceneName);
        }
        Save();
    }
    
    public void SetLevelClear() {
        SetLevelClear(currentLevel);
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
            maxJumps = 1;
        } else {
            JsonUtility.FromJsonOverwrite(
                PlayerPrefs.GetString("GameData"),
                Instance
            );
        }
    }

    private void Update() { }
}