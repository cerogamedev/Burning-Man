using System.Collections;
using System.Collections.Generic;
using BurningMan.Controller;
using Unity.VisualScripting;
using UnityEngine;

namespace BurningMan
{
    public class IIdle : IPlayerState
    {
        public override void EnterState(PlayerController player)
        {
            player.anim.Play("mainCharacter_idle");
        }

        public override void ExitState(PlayerController player)
        {

        }

        public override void UpdateState(PlayerController player)
        {
            if (LeftButtonPressed.Instance.IsLeftButtonPressed || RightButtonController.Instance.IsRightButtonPressed)
            {
                player.ChangeState(new IWalk());
            }
            if (Mathf.Abs( player.rb.velocity.y)>0.2f) {player.ChangeState(new IJump());}
        }
    }
}
