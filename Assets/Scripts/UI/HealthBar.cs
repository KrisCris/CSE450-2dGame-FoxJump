using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HealthBar : MonoBehaviour {
        public Image fgImage;
        public Image bgImage;

        // private float _healthPercent = 1;

        private float _maxHealth;
        private float _curHealth;

        public float fgSpeed = 1f;
        public float bgSpeed = 0.1f;

        // Update is called once per frame
        public void Init(float maxHealth) {
            _maxHealth = maxHealth;
            _curHealth = maxHealth;
        }

        public void UpdateHealthBar(float curHealth, float maxHealth) {
            _curHealth = curHealth;
            _maxHealth = maxHealth;
        }

        void Update() {
            int op = 1;
            float targetValue = _curHealth / _maxHealth;
            float fgOffset = targetValue - fgImage.fillAmount;
            float bgOffset = targetValue - bgImage.fillAmount;
            if (fgOffset < 0) op = -1;

            float dFg = Mathf.Min(Time.deltaTime * fgSpeed, fgOffset * op);
            float dBg = Mathf.Min(Time.deltaTime * bgSpeed, bgOffset * op);

            fgImage.fillAmount += dFg * op;
            bgImage.fillAmount += dBg * op;
        }
    }
}