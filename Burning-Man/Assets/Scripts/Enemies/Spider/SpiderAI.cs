using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class SpiderAI : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float _speed;
        private int index=0;
        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[index].transform.position,_speed/20);
            if (Vector2.Distance(transform.position, patrolPoints[index].transform.position)<0.3f)
            {
                index++;
                if (index==patrolPoints.Length){index = 0;}
            }
            if (transform.position.y > patrolPoints[index].transform.position.y) {transform.localScale = new Vector3(1f,-1f,1f);}
            else{transform.localScale = new Vector3(1f,1f,1f);}
        }
    }
}
