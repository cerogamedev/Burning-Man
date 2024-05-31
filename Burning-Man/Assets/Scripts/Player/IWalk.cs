using System.Collections;
using System.Collections.Generic;
using BurningMan.Controller;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace BurningMan
{
    public class IWalk : IPlayerState
    {
        public override void EnterState(PlayerController player)
        {
            player.anim.Play("mainCharacter_Walk");
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


            if (!LeftButtonPressed.Instance.IsLeftButtonPressed && !RightButtonController.Instance.IsRightButtonPressed && player.isGrounded)
            {
                player.ChangeState(new IIdle());
            }
            if (!player.isGrounded) {player.ChangeState(new IJump());}
            
        }
    }
}
