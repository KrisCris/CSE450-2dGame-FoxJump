using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using Item;
using UnityEngine;

namespace Item {
    public class FlyingShoes : Item {
        private void Awake() {
            IsFloat = false;
            ItemClass = Items.FlyingShoes;
        }
    }
}
