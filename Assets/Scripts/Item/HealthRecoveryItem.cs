using Entity.Player;

namespace Item {
    public class HealthRecoveryItem : Item {
        public float health = 3;

        protected override void OnItemCollect(PlayerEntity player) {
            player.gameObject.SendMessage("OnHealing", health);
        }
    }
}
