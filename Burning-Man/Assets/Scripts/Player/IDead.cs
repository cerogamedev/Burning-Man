using System.Collections;
using System.Collections.Generic;
using BurningMan.Controller;
using UnityEngine;

namespace BurningMan
{
    public class IDead : IPlayerState
    {
        public override void EnterState(PlayerController player)
        {
            player.anim.Play("mainCharacter_dead");
        }

        public override void ExitState(PlayerController player)
        {
        }

        public override void UpdateState(PlayerController player)
        {
            
        }

    }
}
