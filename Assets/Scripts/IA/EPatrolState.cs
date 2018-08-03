using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPatrolState : State<EnemyInput>
{

    public Enemy enemy;

    public EPatrolState(Enemy p)
    {
        enemy = p;
    }
    public override void Enter()
    {
        enemy.Patrol();
        //base.Enter();
    }
}
