using System;
using UnityEngine;

namespace Entity {
    public class VFXDestroy : MonoBehaviour{
        private void Start() {
            Destroy(gameObject, 1);
        }
    }
}