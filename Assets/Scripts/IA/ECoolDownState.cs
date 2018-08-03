using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECoolDownState : State<EnemyInput>
{

    public Enemy enemy;

    public ECoolDownState(Enemy c)
    {
        enemy = c;
    }
    public override void Enter()
    {
        enemy.CoolDown();
        //base.Enter();
    }
}
