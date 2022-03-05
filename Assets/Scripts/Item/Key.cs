using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using Item;
using UnityEngine;

namespace Item {
    public class Key : Item
    {

        private void Awake() {
            ItemClass = Items.Key;
        }
    } 
}
