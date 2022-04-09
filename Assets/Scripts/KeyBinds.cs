using System;
using UnityEngine;

[Serializable]
public class KeyBinds: MonoBehaviour {
    public static KeyBinds Instance;

    public KeyCode climb = KeyCode.W;
    public KeyCode jump = KeyCode.Space;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode interaction = KeyCode.E;
    public KeyCode attack = KeyCode.Mouse0;
    
    private void Awake() {
        Instance = this;
    }
}