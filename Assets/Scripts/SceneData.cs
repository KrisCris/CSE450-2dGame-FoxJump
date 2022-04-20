using Entity.Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneData: MonoBehaviour {
    public Transform spawnPoint;
    public string sceneName;
    public string messages = "";
    private void Start() {
        bool useSavePoint = false;
        if (CameraController.Instance && gameObject.TryGetComponent(out PolygonCollider2D vcamBoundingBox)) {
            CameraController.Instance.UpdateConfiner(vcamBoundingBox);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        PlayerEntity playerEntity;
        if (playerEntity = FindObjectOfType<PlayerEntity>()) {
            var pos = spawnPoint.position;
            if (GameController.Instance && GameController.Instance.ValidateSavePoint()) {
                pos = GameController.Instance.GetLastSavePoint();
                useSavePoint = true;
            }
            playerEntity.UpdatePos(new Vector3(pos.x, pos.y, 0));
            playerEntity.SetEventTrigger(false);
            playerEntity.SetColliderState(true);
            playerEntity.showDeath = true;
            playerEntity.Animator.SetBool("IsDead", false);
            playerEntity.NoSpeed();
            playerEntity.UnsetClimbable();
        }

        if (messages.Length > 0 && MessageController.Instance && !useSavePoint) {
            MessageController.Instance.ShowMessage(messages);
        }

        Time.timeScale = 1;
    }
}
