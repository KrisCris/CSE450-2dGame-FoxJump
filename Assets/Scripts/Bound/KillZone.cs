using UnityEngine;

namespace Bound {
    public class KillZone : MonoBehaviour {
        public float dmg = 10;
        private void OnTriggerStay2D(Collider2D others) {
            if (others.CompareTag("Player")) others.gameObject.SendMessage("OnDamage", dmg);
        }
    }
}