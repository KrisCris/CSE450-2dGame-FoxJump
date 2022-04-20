using System;
using System.Collections.Generic;
using Entity.Player;
using UnityEngine;

namespace Ground {
    public class ItemTrigger : MonoBehaviour {
        public List<GameObject> triggerableItems;
        private bool _triggered;

        private void Awake() {
            _triggered = false;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.GetComponent<PlayerEntity>() && !_triggered) {
                foreach (GameObject triggerableItem in triggerableItems) {
                    if (!triggerableItem.GetComponent<ITriggerable>().HasTriggered()) {
                        triggerableItem.GetComponent<ITriggerable>().Trigger();
                    }
                }

                _triggered = true;
            }

        }
    }
}