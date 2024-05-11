using System.Collections;
using System.Collections.Generic;
using BurningMan.Controller;
using UnityEngine;

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
                player.transform.Translate(Vector2.left * player._moveSpeed * Time.deltaTime);
                player.transform.localScale =new (-1f,1f,1f);            
            }
            if (RightButtonController.Instance.IsRightButtonPressed) 
            { 
                player.transform.Translate(Vector2.right * player._moveSpeed * Time.deltaTime);
                player.transform.localScale =new (1f,1f,1f);
            }


            if (player._goToLeftDone)
            {
                player.transform.Translate(Vector2.left * player._lastSpeedInUse * Time.deltaTime);
                player._lastSpeedInUse -= 0.1f;
            }
            if (player._goToRightDone)
            {
                player.transform.Translate(Vector2.right * player._lastSpeedInUse * Time.deltaTime);
                player._lastSpeedInUse -= 0.1f;

            }
            if (player._lastSpeedInUse < 0.2f)
            {
                player._goToLeftDone = false;
                player._goToRightDone = false;
                player._lastSpeedInUse = 0;
            }

            if (!LeftButtonPressed.Instance.IsLeftButtonPressed && !RightButtonController.Instance.IsRightButtonPressed && player.isGrounded)
            {
                player.ChangeState(new IIdle());
            }
            if (!player.isGrounded) {player.ChangeState(new IJump());}
            
        }
    }
}
