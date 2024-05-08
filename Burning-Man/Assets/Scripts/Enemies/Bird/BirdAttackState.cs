using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class BirdAttackState : IFlyState
    {
        public void EnterState(EnemyFlySM enemyControl)
        {
            enemyControl.anim.Play("enemy_bird_attack");
            enemyControl.AttackObject.SetActive(true);
        }

        public void ExitState(EnemyFlySM enemyControl)
        {
            enemyControl.anim.Play("enemy_bird_fly");
            enemyControl.AttackObject.SetActive(false);
        }

        public void UpdateState(EnemyFlySM enemyControl)
        {
            if (Vector2.Distance(enemyControl.transform.position, enemyControl.Player.transform.position)>0.5f)
            {
                enemyControl.ChangeState(new BirdFollowState());
            }
        }
    }
}
