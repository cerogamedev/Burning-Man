using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class BirdPatrollState : IFlyState
    {
        private int index;
        public void EnterState(EnemyFlySM enemyControl)
        {
            index=0;
        }

        public void ExitState(EnemyFlySM enemyControl)
        {
        }

        public void UpdateState(EnemyFlySM enemyControl)
        {
            if (enemyControl.IsPlayerHere) {enemyControl.ChangeState(new BirdFollowState());}
            if (Vector2.Distance(enemyControl.transform.position, enemyControl.PatrollPoints[index].transform.position)< 0.3f)
            {
                index++;
                if (index == enemyControl.PatrollPoints.Length)
                {
                    index = 0;
                }
            }
            enemyControl.transform.position = Vector2.MoveTowards(enemyControl.transform.position, enemyControl.PatrollPoints[index].transform.position,enemyControl.FlySpeed/10);
        }
    }
}
