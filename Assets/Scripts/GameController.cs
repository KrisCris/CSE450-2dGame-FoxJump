using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour {
    public static GameController Instance;
    public string currentLevel;

    public List<string> selectableLevel;
    // public HashSet<string> selectableLevel;

    private void Awake() {
        Instance = this;
        Load();
    }

    private void Start() { }
    
    public void SetLevelClear(string sceneName) {
        if (!selectableLevel.Contains(sceneName)) {
            selectableLevel.Add(sceneName);
        }
        Save();
    }

    public void SetCurrentScene(string sceneName) {
        currentLevel = sceneName;
        Save();
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