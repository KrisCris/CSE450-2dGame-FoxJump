using UnityEngine;

namespace Code.Entity.Player
{
    public class PlayerEntity : JumpableEntity
    {
        public KeyCode keyUp = KeyCode.W;
        public KeyCode keyDown = KeyCode.S;
        public KeyCode keyLeft = KeyCode.A;
        public KeyCode keyRight = KeyCode.D;
        public KeyCode keyJump = KeyCode.Space;
        
        private void Update()
        {
            if (Input.GetKey(keyUp))
            {
                // TODO climb?
            }

            if (Input.GetKey(keyDown))
            {
                // TODO climb down?
                // TODO crouch?
                // TODO maybe some skills
            }

            if (Input.GetKey(keyLeft))
            {
                Rigidbody2D.AddForce(Vector2.left * 12f * Time.deltaTime, ForceMode2D.Impulse);
            }

            if (Input.GetKey(keyRight))
            {
                Rigidbody2D.AddForce(Vector2.right * 12f * Time.deltaTime, ForceMode2D.Impulse);
            }
            
            // jump
            if (Input.GetKeyDown(keyJump))
            {
                if (CurrJumps > 0)
                {
                    --CurrJumps;
                    Rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                }
            }
        }
    }
}