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
    public bool canWallClimb;
    public Vector2 lastSavePoint;

    public List<string> selectableLevel;

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
        canWallClimb = false;
        lastSavePoint = new Vector2(-9999f, -9999f);
        Save();
        // Debug.Log("new game init");
    }

    public bool ValidateSavePoint() {
        return lastSavePoint.x > -9999 && lastSavePoint.y > -9999;
    }

    public void AddCoins(int num) {
        coins += num;
        // Save();
    }

    public void AddKeys(int num) {
        keys += num;
        // Save();
    }

    public void AddJumps(int num) {
        maxJumps += num;
        // Save();
    }
    
    public void SetLevelClear(string sceneName) {
        if (!selectableLevel.Contains(sceneName)) {
            selectableLevel.Add(sceneName);
        }
        lastSavePoint = new Vector2(-9999, -9999);
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

    public Vector2 GetLastSavePoint() {
        return lastSavePoint;
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
}