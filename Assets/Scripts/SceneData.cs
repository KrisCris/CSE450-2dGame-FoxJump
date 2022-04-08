using System;
using UnityEngine;
public class SceneData: MonoBehaviour {
    private void Start() {
        if (CameraController.Instance && gameObject.TryGetComponent(out PolygonCollider2D vcamBoundingBox)) {
            CameraController.Instance.UpdateConfiner(vcamBoundingBox);
        }
    }
}
