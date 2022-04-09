using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using UnityEngine;

namespace Item {
    public class healthRecovery : Item {
        public float recoveryHealthAmount;
        private void Awake() {
            IsFloat = false;
            ItemClass = Items.RecoveryBlood;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>()) {
                other.gameObject.SendMessage("OnHealing", recoveryHealthAmount);
                base.OnTriggerStay2D(other);
            }
        }
    }
}
