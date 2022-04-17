using Entity.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneData: MonoBehaviour {
    public Transform spawnPoint;
    public string sceneName;
    private void Start() {
        if (CameraController.Instance && gameObject.TryGetComponent(out PolygonCollider2D vcamBoundingBox)) {
            CameraController.Instance.UpdateConfiner(vcamBoundingBox);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        PlayerEntity playerEntity;
        if (playerEntity = FindObjectOfType<PlayerEntity>()) {
            var pos = spawnPoint.position;
            playerEntity.UpdatePos(new Vector3(pos.x, pos.y, 0));
            playerEntity.SetEventTrigger(false);
            playerEntity.SetColliderState(true);
            playerEntity.showDeath = true;
            playerEntity.Animator.SetBool("IsDead", false);
        }
    }
}
