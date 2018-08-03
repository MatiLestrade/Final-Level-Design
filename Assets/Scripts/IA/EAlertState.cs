using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAlertState : State<EnemyInput> {

    public Enemy enemy;

    public EAlertState(Enemy e)
    {
        enemy = e;
    }
    public override void Enter()
    {
        enemy.Alert();
        //base.Enter();
    }
}
