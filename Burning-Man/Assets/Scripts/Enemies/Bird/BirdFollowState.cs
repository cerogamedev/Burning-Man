using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class BirdFollowState : IFlyState
    {
        public void EnterState(EnemyFlySM enemyControl)
        {
        }

        public void ExitState(EnemyFlySM enemyControl)
        {
        }

        public void UpdateState(EnemyFlySM enemyControl)
        {
            enemyControl.transform.position = Vector2.MoveTowards(enemyControl.transform.position, enemyControl.Player.transform.position, enemyControl.FlySpeed/10);
            if (Vector2.Distance(enemyControl.transform.position, enemyControl.Player.transform.position)>enemyControl.FollowRadius)
            {
                enemyControl.ChangeState(new BirdPatrollState());
            }
            if (Vector2.Distance(enemyControl.transform.position, enemyControl.Player.transform.position)<0.3f)
            {
                enemyControl.ChangeState(new BirdAttackState());
            }
        }
    }
}
