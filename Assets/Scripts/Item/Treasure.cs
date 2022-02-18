using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Entity.Player;
using UnityEngine;

namespace Item
{
    public class Treasure : MonoBehaviour
    {
        private Animator animator;

        private void OnTriggerEnter2D(Collider2D other)
        {
            animator.SetBool("isOpen", true);
            if (other.gameObject.GetComponent<PlayerEntity>())
            {
                animator.SetBool("isOpen", true);
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            print(animator.GetBool("isOpen"));
        }
    }
}
