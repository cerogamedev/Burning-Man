using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class DestroyableTile : MonoBehaviour, IDestroyable
    {
        public GameObject ParticleEffect;
        public void Destroy()
        {
            GameObject effect = Instantiate(ParticleEffect, transform.position, transform.rotation);
            Destroy(effect, 3f);
            Destroy(this.gameObject);
        }
    }
}
