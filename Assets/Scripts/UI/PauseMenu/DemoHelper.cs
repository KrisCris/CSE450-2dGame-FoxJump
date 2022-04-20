using System;
using Entity.Player;
using Item;
using UnityEngine;

namespace UI.PauseMenu {
    public class DemoHelper : MonoBehaviour {
        public GameObject foxTail;
        public GameObject doubleJumpShoes;
        public GameObject key;
        public GameObject menu;
        private bool _active;


        private void Start() {
            menu.SetActive(false);
            _active = false;
        }

        private void LateUpdate() {
            if (GameController.Instance && GameController.Instance.demoMode && !_active) {
                menu.SetActive(true);
                _active = true;
            }
        }

        public void InfHealth() {
            PlayerEntity p;
            if (p = FindObjectOfType<PlayerEntity>()) {
                p.maxHealth = 9999;
                p.OnHealing(10000);
            }
        }

        public void RegularHealth() {
            PlayerEntity p;
            if (p = FindObjectOfType<PlayerEntity>()) {
                p.maxHealth = 15;
                p.health = 15;
                p.OnHealing(15);
            }
        }

        public void GetDoubleJump() {
            PlayerEntity p;
            if (p = FindObjectOfType<PlayerEntity>()) {
                Instantiate(doubleJumpShoes, p.gameObject.transform.position, Quaternion.identity);
            }
        }

        public void GetFoxTail() {
            PlayerEntity p;
            if (p = FindObjectOfType<PlayerEntity>()) {
                Instantiate(foxTail, p.gameObject.transform.position, Quaternion.identity);
            }
        }

        public void GetKey() {
            PlayerEntity p;
            if (p = FindObjectOfType<PlayerEntity>()) {
                Instantiate(key, p.gameObject.transform.position, Quaternion.identity);
            }
        }

        public void GetCoins() {
            PlayerEntity p;
            if (p = FindObjectOfType<PlayerEntity>()) {
                p.OnItemCollect(Items.Coin, 100);
            }
        }

        public void GetProjectile() {
            PlayerEntity p;
            if (p = FindObjectOfType<PlayerEntity>()) {
                p.OnItemCollect(Items.Projectile, 100);
            }
        }

        public void Scene_0_1() {
            SceneController.Instance.SwitchMap("Scene_0_1");
        }

        public void Scene_0_1_End() {
            SceneData s;
            if (s = FindObjectOfType<SceneData>()) {
                if (s.sceneName == "Scene_0_1") {
                    PlayerEntity p;
                    if (p = FindObjectOfType<PlayerEntity>()) {
                        p.transform.position = new Vector3(220f, 12, 0);
                    }
                }
            }
        }

        public void Scene_3() {
            SceneController.Instance.SwitchMap("Scene_3");
        }

        public void Map_1() {
            SceneController.Instance.SwitchMap("Map_1");
        }

        public void BOSS() {
            SceneData s;
            if (s = FindObjectOfType<SceneData>()) {
                if (s.sceneName == "Map_1") {
                    PlayerEntity p;
                    if (p = FindObjectOfType<PlayerEntity>()) {
                        p.transform.position = new Vector3(-4.48f, 16, 0);
                    }
                }
            }
        }

        public void EndGameMap() {
            SceneController.Instance.SwitchMap("Scene_0_2");
        }
    }
}