using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BurningMan
{
    public class EnemyFlySM : MonoBehaviour
    {
        private IFlyState currentState;
        public Transform[] PatrollPoints;
        public float FlySpeed;
        private BoxCollider2D _collider;
        public bool IsPlayerHere = false;
        public Transform Player;
        public float FollowRadius;
        public Animator anim;
        public GameObject AttackObject;
        void Awake()
        {
            AttackObject = this.transform.GetChild(0).gameObject;
            AttackObject.SetActive(false);

            anim = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        void Start()
        {
            ChangeState(new BirdPatrollState());
        }

        void Update()
        {
            currentState.UpdateState(this);
        }
        public void ChangeState(IFlyState newState)
        {
            currentState?.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) {IsPlayerHere = true;}
        }
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player")) {IsPlayerHere = false;}

        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, FollowRadius);
        }
        
    }
}
