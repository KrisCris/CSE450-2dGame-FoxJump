using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MessageController : MonoBehaviour {
        public static MessageController Instance;
        
        public GameObject messagePanel;
        public TextMeshProUGUI messageText;

        public GameObject clickablePanel;
        public Button clickableButton;
        public TextMeshProUGUI clickableText;

        private Messages _messageHolder = null;
        
        private bool _messageOn = false;
        private bool _clickableOn = false;

        public delegate void Callback();

        public delegate void ClickableEvent(Button btn);
        
        private Callback _f = null;
        
        private void Awake() {
            Instance = this;
        }

        private void Start() {
            HideMessage();
            HideClickable();
        }

        private void Update() {
            if (clickablePanel && _clickableOn) {
                if (Input.GetKeyDown(KeyCode.C)) {
                    HideClickable();
                }
            }
            
            if (messagePanel && _messageOn) {
                if (Input.GetKeyDown(KeyCode.C)) {
                    if (_messageHolder == null) {
                        HideMessage();
                    } else {
                        if (!_messageHolder.HasNext()) {
                            _messageHolder = null;
                            HideMessage();
                        } else {
                            ShowMessage(_messageHolder.Next());
                        }
                    }
                }
            }
        }

        public void ShowMessage(string msg, Callback f = null) {
            if (_clickableOn) {
                HideClickable();
            }
            if (messagePanel && messageText && msg != null) {
                messageText.text = msg;
                messagePanel.SetActive(true);
                _messageOn = true;
                if (f != null) {
                    _f = f;
                }
            } else {
                HideMessage();
            }
        }

        public void ShowMessage(Messages msg, Callback f = null) {
            _messageHolder = msg;
            if (f != null) {
                _f = f;
            }
            ShowMessage(_messageHolder.Next());
        }

        public void HideMessage(bool force = false) {
            if (messagePanel && messageText) {
                if (_messageHolder != null && _messageHolder.HasNext()) {
                    _messageHolder.RevertLast();
                    _messageHolder = null;
                }
                messageText.text = "";
                messagePanel.SetActive(false);
                _messageOn = false;
                if (_f != null && !force) {
                    _f();
                }
                _f = null;
            }
        }

        public void HideClickable() {
            if (clickablePanel && clickableButton && clickableText) {
                clickableButton.onClick.RemoveAllListeners();
                clickableText.text = "";
                clickablePanel.SetActive(false);
                _clickableOn = false;
            }
        }

        public void ShowClickable(string text, ClickableEvent f) {
            if (_messageOn) {
                HideMessage(true);
            }
            if (clickablePanel && clickableButton && clickableText) {
                clickableButton.onClick.AddListener(() => {
                    f(clickableButton);
                });
                clickableText.text = text;
                clickablePanel.SetActive(true);
                _clickableOn = true;
            }
        }

        public bool isMessageOn() {
            return _messageOn;
        }

        public bool isClickableOn() {
            return _clickableOn;
        }
    }
}