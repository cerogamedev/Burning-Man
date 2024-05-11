using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan.Controller
{
    public class PlayerController : MonoSingleton<PlayerController>
    {
        [Header("Player Walk")]
        public float _moveSpeed;
        [SerializeField] public float _lastSpeed;
        public float _lastSpeedInUse;
        public bool _goToLeftDone = false;
        public bool _goToRightDone = false;

        [Header("PlayerJump")]
        [SerializeField] public float _jumpForce;
        [SerializeField] public LayerMask jumpableGround;
        [SerializeField] private float _maxVelocity; 
        public bool isGrounded;


        //Components
        public Rigidbody2D rb;
        private BoxCollider2D coll;
        public Animator anim;
        private IPlayerState currentState;

        private void OnEnable()
        {
            LeftButtonPressed.IsLeftPressDone += GotToLeftDone;
            RightButtonController.IsRightPressDone += GotToRightDone;

            JumpButtonController.JumpButtonPressed += JumpPressed;

        }
        void Awake()
        {
            anim = GetComponent<Animator>();
            coll = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            ChangeState(new IIdle());
        }
        public void ChangeState(IPlayerState newState)
        {
            currentState?.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
        }
        private void Update()
        {
            isGrounded = IsGrounded();
            currentState.UpdateState(this);
        }
        public void FixedUpdate()
        {
            if (rb.velocity.magnitude > _maxVelocity)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, _maxVelocity);
            } 
        }

        public void GotToLeftDone()
        {
            _lastSpeedInUse = _lastSpeed;
            _goToLeftDone = true;
        }

        public void GotToRightDone()
        {
            _lastSpeedInUse = _lastSpeed;
            _goToRightDone = true;
        }
        public bool IsGrounded()
        {
            if ( coll  != null)
            {
                return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .2f, jumpableGround);
            }
            else {return false;}
        }

        public void JumpPressed()
        {
            if (rb!=null)
            {
                if (IsGrounded())
                {
                    rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
                if (!IsGrounded() && PlayerAttack.Instance.CurrentBullet > 0) // add  if gun isn't empty
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * _jumpForce/4, ForceMode2D.Impulse);
                    PlayerAttack.Instance.FireBullet();
                }
            }

        }

    }

}
