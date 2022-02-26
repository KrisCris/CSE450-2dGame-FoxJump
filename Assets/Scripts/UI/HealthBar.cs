using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HealthBar : MonoBehaviour {
        public Image hpImage;
        public Image hpEffect;

        // private float _healthPercent = 1;

        private float _maxHealth;
        private float _curHealth;

        // [HideInInspector] public float hp;
        // [SerializeField] private float maxHp;
        // [SerializeField] private float hurtsSpeed = 0.001f;

        public float imgSpeed = 0.01f;
        public float efxSpeed = 0.001f;

        // Start is called before the first frame update
        // void Start()
        // {
        //     hp = maxhp;
        // }

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
            float shouldFill = _curHealth / _maxHealth;
            
            hpImage.fillAmount = hpImage.fillAmount > shouldFill
                ? hpImage.fillAmount - imgSpeed
                : hpImage.fillAmount;

            hpEffect.fillAmount = hpEffect.fillAmount > shouldFill
                ? hpEffect.fillAmount - efxSpeed
                : hpEffect.fillAmount;
            // if (hpEffect.fillAmount > shouldFill) {
            //     hpEffect.fillAmount -= hurtsSpeed;
            // } else {
            //     hpEffect.fillAmount = hpImage.fillAmount;
            // }
        }
    }
}