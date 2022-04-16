using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.Enemy {
    public class Boss : Entity
    {
        // Outlet
        public GameObject arm;
        public float attackFrequent;
        public Transform armPosition;
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        // State Track
        
        

        void ArmAttack() {
            
        }

        protected override void OnDeath(string reason) {
            Animator.SetBool(IsDead, true);
        }
    }
    
}
