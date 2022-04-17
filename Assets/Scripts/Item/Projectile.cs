using System;

namespace Item {
    public class Projectile : Item {
        private void Awake() {
            itemClass = Items.Projectile;
        }
    }
}