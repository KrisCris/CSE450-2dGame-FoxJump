// using UnityEngine;
// using UnityEngine.UI;
//
// namespace UI {
//     public class StartGameButton : MonoBehaviour {
//         public Button startGame;
//
//         void Start() {
//             startGame.onClick.AddListener(() => {
//                 SceneController.Instance.LoadGame("Scene_0");
//             });
//         }
//
//         // void StartGame()
//         // {
//         //     SceneManager.UnloadSceneAsync("MainUI");
//         //     SceneManager.LoadScene("Player", LoadSceneMode.Additive);
//         //     SceneManager.LoadScene("Scene_0", LoadSceneMode.Additive);
//         //     
//         //     // while (true)
//         //     // {
//         //     //     if (SceneManager.GetSceneByName("Scene_0").isLoaded)
//         //     //     {
//         //     //         SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scene_0"));
//         //     //         break;
//         //     //     }
//         //     // }
//         //     // SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scene_0"));
//         //
//         //     Time.timeScale = 1;
//         // }
//     }
// }