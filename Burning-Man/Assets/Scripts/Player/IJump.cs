using System.Collections;
using System.Collections.Generic;
using BurningMan.Controller;
using UnityEngine;

namespace BurningMan
{
    public class IJump : IPlayerState
    {
        public override void EnterState(PlayerController player)
        {
        }

        public override void ExitState(PlayerController player)
        {
        }

        public override void UpdateState(PlayerController player)
        {
            if (LeftButtonPressed.Instance.IsLeftButtonPressed) 
            { 
                player.rb.AddForce(Vector2.left*player._moveSpeed * Time.deltaTime, ForceMode2D.Force);
                player.transform.localScale =new (-1f,1f,1f);            
            }
            if (RightButtonController.Instance.IsRightButtonPressed) 
            { 
                player.rb.AddForce(Vector2.right*player._moveSpeed * Time.deltaTime, ForceMode2D.Force);
                player.transform.localScale =new (1f,1f,1f);
            }
            if (player.rb.velocity.y < -0.2f) {player.anim.Play("mainCharacter_jumpdown");}
            if (player.rb.velocity.y > 0.2f) {player.anim.Play("mainCharacter_jumpup");}

            if (player.isGrounded) {player.ChangeState(new IIdle());}
        }
    }
}
