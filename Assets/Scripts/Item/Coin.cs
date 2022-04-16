using Entity.Player;
using UnityEngine;

namespace Item {
    public class Coin : Item {
        private void Awake() {
            ItemClass = Items.Coin;
        }
        protected override void PlayCollectionSound() {
            SoundControlInst.PlaySound(SoundControlInst.coinCollected);
        }
    }
}