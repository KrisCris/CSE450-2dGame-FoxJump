using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using Item;
using UI;
using UnityEngine;

namespace Item {
    public class FlyingShoes : Item {
        protected override void OnItemCollect(PlayerEntity player) {
            base.OnItemCollect(player);
            if (MessageController.Instance) {
                MessageController.Instance.ShowMessage("You Learned Double Jump!");
            }
        }
    }
}
