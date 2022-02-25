using UnityEngine;
using UnityEngine.UI;

namespace Entity.Enemy {
    public class HealthBar : MonoBehaviour {
        public Image hpImage;
        public Image hpEffect;

        private float _healthPercent = 1;

        // [HideInInspector] public float hp;
        // [SerializeField] private float maxHp;
        [SerializeField] private float hurtsSpeed = 0.001f;
        // Start is called before the first frame update
        // void Start()
        // {
        //     hp = maxhp;
        // }

        // Update is called once per frame

        public void UpdateHealthBar(float percentage) {
            this._healthPercent = percentage;
        }
    
        void Update() {
            hpImage.fillAmount = _healthPercent;
            if (hpEffect.fillAmount > hpImage.fillAmount) {
                hpEffect.fillAmount -= hurtsSpeed;
            } else {
                hpEffect.fillAmount = hpImage.fillAmount;
            }
        }
    }
}
