using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EChaseState : State<EnemyInput>
{

    public Enemy enemy;

    public EChaseState(Enemy c)
    {
        enemy = c;
    }
    public override void Enter()
    {
        enemy.Chase();
     
        //base.Enter();
    }
}
