using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public interface IFlyState
    {
        void EnterState(EnemyFlySM enemyControl);
        void ExitState(EnemyFlySM enemyControl);
        void UpdateState(EnemyFlySM enemyControl);

    }
}
