using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using UnityEngine;

namespace Item {
    public class healthRecovery : Item {
        private void Awake() {
            IsFloat = false;
            ItemClass = Items.FlyingShoes;
        }
    }
}
