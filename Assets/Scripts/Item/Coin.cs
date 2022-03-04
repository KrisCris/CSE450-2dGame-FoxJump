using Entity.Player;
using UnityEngine;

namespace Item {
    public class Coin : Item {
        private void Awake() {
            ItemClass = Items.Coin;
        }
    }
}