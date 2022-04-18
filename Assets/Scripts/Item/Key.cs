using Entity.Player;
using UI;

namespace Item {
    public class Key : Item {
        protected override void OnItemCollect(PlayerEntity player) {
            if (MessageController.Instance && GameController.Instance && !GameController.Instance.hasKey) {
                MessageController.Instance.ShowMessage("The missing key of a chest...");
            }
            base.OnItemCollect(player);
        }
    }
}