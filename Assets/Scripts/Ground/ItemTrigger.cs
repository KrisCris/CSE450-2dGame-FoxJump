using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ground {
    public class ItemTrigger : MonoBehaviour {
        public List<GameObject> triggerableItems;

        private void OnTriggerEnter2D(Collider2D col) {
            foreach (GameObject triggerableItem in triggerableItems) {
                if (!triggerableItem.GetComponent<ITriggerable>().HasTriggered()) {
                    triggerableItem.GetComponent<ITriggerable>().Trigger();
                }
            }
        }
    }
}