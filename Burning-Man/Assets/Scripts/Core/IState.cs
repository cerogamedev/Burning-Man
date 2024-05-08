using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BurningMan.Controller;

namespace BurningMan
{
    public interface IState
    {
        void EnterState(PlayerController player);
        void ExitState(PlayerController  player);
        void UpdateState (PlayerController player);
        
    }
}
