using Entity.Player;
using UI;

namespace Item {
    public class FlyingShoes : Item {
        protected override void OnItemCollect(PlayerEntity player) {
            if (MessageController.Instance && GameController.Instance && !GameController.Instance.hasJumpShoes) {
                MessageController.Instance.ShowMessage("You Learned Double Jump!");
            }
            base.OnItemCollect(player);
        }
    }
}
