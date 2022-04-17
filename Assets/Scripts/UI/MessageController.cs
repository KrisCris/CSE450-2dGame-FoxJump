using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI {
    public class MessageController : MonoBehaviour {
        public static MessageController Instance;
        
        public GameObject panel;
        public TextMeshProUGUI messageText;

        public delegate void Callback();

        private Callback _f = null;
        
        private void Awake() {
            Instance = this;
        }

        private void Start() {
            HideMessage();
        }

        private void Update() {
            if (panel) {
                if (Input.GetKey(KeyCode.C)) {
                    HideMessage();
                }
            }
        }

        public void ShowMessage(string msg, Callback f = null) {
            if (panel && messageText) {
                messageText.text = msg;
                panel.SetActive(true);
                _f = f;
            }
        }

        public void HideMessage() {
            if (panel && messageText) {
                messageText.text = "";
                panel.SetActive(false);
                if (_f != null) {
                    _f();
                    _f = null;
                }
            }
        }
    }
}