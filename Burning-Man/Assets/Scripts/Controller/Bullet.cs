using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class Bullet : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<IDestroyable>() != null)
            {
                other.GetComponent<IDestroyable>().Destroy();
                DeadSceneManager.Instance.DeadCount+=1;
                Destroy(this);
            }
        }
    }
}
