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
            var pos = spawnPoint.position;
            playerEntity.UpdatePos(new Vector3(pos.x, pos.y, 0));
        }
    }
}
