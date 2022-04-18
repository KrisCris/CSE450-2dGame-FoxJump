using Entity.Player;
using UI;

namespace Item {
    public class FoxTail : Item {
        protected override void OnItemCollect(PlayerEntity player) {
            if (MessageController.Instance && GameController.Instance && !GameController.Instance.hasFoxTail) {
                MessageController.Instance.ShowMessage("A tail from the fox that is good at climb walls in the past...");
            }
            base.OnItemCollect(player);
        }
    }
}