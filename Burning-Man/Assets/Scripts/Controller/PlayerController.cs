using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan.Controller
{
    public class PlayerController : MonoSingleton<PlayerController>
    {
        [Header("Player Walk")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _lastSpeed;
        private float _lastSpeedInUse;
        private bool _goToLeftDone = false;
        private bool _goToRightDone = false;

        [Header("PlayerJump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask jumpableGround;
        [SerializeField] private float _maxVelocity; 


        //Components
        private Rigidbody2D rb;
        private BoxCollider2D coll;

        private void OnEnable()
        {
            coll = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            LeftButtonPressed.IsLeftPressDone += GotToLeftDone;
            RightButtonController.IsRightPressDone += GotToRightDone;

            JumpButtonController.JumpButtonPressed += JumpPressed;

        }
        private void Update()
        {
            WalkControll();
            IsGrounded();
        }
        public void FixedUpdate()
        {
            if (rb.velocity.magnitude > _maxVelocity)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, _maxVelocity);
            } 
        }
        #region Walk Controll Region
        public void WalkControll()
        {
            if (LeftButtonPressed.Instance.IsLeftButtonPressed) { GoToLeft(); }
            if (RightButtonController.Instance.IsRightButtonPressed) { GoToRight(); }

            if (_goToLeftDone)
            {
                transform.Translate(Vector2.left * _lastSpeedInUse * Time.deltaTime);
                _lastSpeedInUse -= 0.1f;
            }
            if (_goToRightDone)
            {
                transform.Translate(Vector2.right * _lastSpeedInUse * Time.deltaTime);
                _lastSpeedInUse -= 0.1f;
            }
            if (_lastSpeedInUse < 0.2f)
            {
                _goToLeftDone = false;
                _goToRightDone = false;
                _lastSpeedInUse = 0;
            }
        }
        public void GoToLeft()
        {
            transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
        }
        public void GotToLeftDone()
        {
            _lastSpeedInUse = _lastSpeed;
            _goToLeftDone = true;
        }
        public void GoToRight()
        {
            transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
        }
        public void GotToRightDone()
        {
            _lastSpeedInUse = _lastSpeed;
            _goToRightDone = true;
        }
        #endregion
        #region Jump Controll Region
        public bool IsGrounded()
        {
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .2f, jumpableGround);
        }

        public void JumpPressed()
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
            if (!IsGrounded()) // add  if gun isn't empty
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * _jumpForce/4, ForceMode2D.Impulse);
            }
        }

        #endregion
    }

}
