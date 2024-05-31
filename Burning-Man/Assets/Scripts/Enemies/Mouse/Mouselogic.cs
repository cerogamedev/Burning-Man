using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class Mouselogic : MonoBehaviour
    {
        public Transform[] GoPoints;
        public float MouseSpeed;
        private float _mouseSpeed;
        private int index = 0;
        void Start()
        {
            _mouseSpeed = Random.Range(MouseSpeed-1, MouseSpeed+1);
        }
        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position,GoPoints[index].position,_mouseSpeed/100f);

            if (Vector2.Distance(transform.position, GoPoints[index].position)<0.2f) {index++;}
            if (index == GoPoints.Length){index = 0;}
            if (GoPoints[index].position.x > transform.position.x){transform.localScale = new (1,1,1);}
            else{transform.localScale = new (-1,1,1);}
        }
    }
}
