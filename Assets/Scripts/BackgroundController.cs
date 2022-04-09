using System;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
    public Transform backgroundImagePos;
    
    private void Start() {
        if (!CameraController.Instance) {
            Debug.Log("BackgroundController::Start >>> CameraController hasn't been initialized!");
        }
    }

    private void Update() {
        if (!backgroundImagePos) {
            Debug.Log("BackgroundController::Update >>> background not found!");
            return;
        }
        backgroundImagePos.position = CameraController.Instance.GetCamPos2D();
    }
}