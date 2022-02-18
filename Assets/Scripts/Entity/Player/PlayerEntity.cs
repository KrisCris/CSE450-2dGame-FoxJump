﻿using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Entity.Player {
    public class PlayerEntity : JumpableEntity {
        public KeyCode keyUp = KeyCode.W;
        public KeyCode keyDown = KeyCode.S;
        public KeyCode keyLeft = KeyCode.A;
        public KeyCode keyRight = KeyCode.D;
        public KeyCode keyJump = KeyCode.Space;
        public KeyCode keyInteraction = KeyCode.E;

        private Dictionary<Items, int> _inventory;
        
        private bool _isCrouching = false;

        private new void Start() {
            base.Start();
            _inventory = new Dictionary<Items, int>();
        }
        
        private void Update() {
            if (Input.GetKey(keyUp)) {
                // TODO climb?
            }

            if (Input.GetKey(keyDown)) {
                // TODO climb down?
                // TODO crouch?
                _isCrouching = true;
                // TODO maybe some skills
            }

            if (Input.GetKey(keyLeft)) {
                Rigidbody2D.AddForce(Vector2.left * 12f * Time.deltaTime, ForceMode2D.Impulse);
            }

            if (Input.GetKey(keyRight)) {
                Rigidbody2D.AddForce(Vector2.right * 12f * Time.deltaTime, ForceMode2D.Impulse);
            }

            // jump
            if (Input.GetKeyDown(keyJump)) {
                if (CurrJumps > 0) {
                    --CurrJumps;
                    Rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                }
            }
            
            Animator.SetBool("isCrouching", _isCrouching);
        }

        public Dictionary<Items, int> GetInventory() {
            return _inventory;
        }

        public void OnItemCollect(Items item, int num) {
            if (_inventory.ContainsKey(item)) {
                _inventory[item] += num;
            }
            else {
                _inventory.Add(item, num);
            }
        }

        public int OnItemUsed(Items item, int num) {
            if (_inventory.ContainsKey(item) && _inventory[item] >= num) {
                _inventory[item] -= num;
                return num;
            }

            return 0;
        }
    }
}