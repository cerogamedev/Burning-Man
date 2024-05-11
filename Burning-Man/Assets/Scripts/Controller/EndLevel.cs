using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class EndLevel : MonoBehaviour
    {
        public static Action _levelEnd;
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) 
            {
                LevelIndex.Instance.IncreaseLevelIndex(1);
                _levelEnd?.Invoke();
            }
        }
    }
}
