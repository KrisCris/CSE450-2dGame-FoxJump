using System;
using Entity.Player;
using UnityEngine;
public class SceneData: MonoBehaviour {
    public Transform spawnPoint;
    private void Start() {
        if (CameraController.Instance && gameObject.TryGetComponent(out PolygonCollider2D vcamBoundingBox)) {
            CameraController.Instance.UpdateConfiner(vcamBoundingBox);
        }

        PlayerEntity playerEntity;
        if (playerEntity = FindObjectOfType<PlayerEntity>()) {
            playerEntity.UpdatePos(spawnPoint.position);
        }
    }
}
