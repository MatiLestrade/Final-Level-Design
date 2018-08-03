using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeedBack : MonoBehaviour {

    public Enemy enemy; 

    public void Chase()
    {
        enemy.timer = 0;
        enemy.speed = 68f;
        enemy.transform.position += enemy.transform.forward * enemy.speed * Time.deltaTime;
        enemy.transform.forward = enemy.player.transform.position - enemy.transform.position;
        enemy.caution = true;
        
    }
    public void CoolDown()
    { //enemy.
        enemy.timerCoolDown += Time.deltaTime;
        enemy.me.material.color = Color.black;

        if (enemy.timerCoolDown > 4) { enemy.timerCoolDown = 0; enemy.coolDown = false; }
    }
    public void Alert()
    {
        enemy.timer += Time.deltaTime;
        enemy.speed = 10;
        enemy.transform.position += enemy.transform.forward * enemy.speed * Time.deltaTime;
        enemy.transform.forward = enemy.player.transform.position - enemy.transform.position;
    }
    public void Patrol()
    {
        if (!enemy.aStar)
        {
            enemy.speed = 55f;
            if (Vector3.Distance(enemy.transform.position, enemy.target.transform.position) <= 1f)
            { enemy.newWaypoint(); }
            enemy.transform.position += enemy.transform.forward * enemy.speed * Time.deltaTime;
            enemy.transform.forward = enemy.target.transform.position - enemy.transform.position;
        }
        else
        { enemy.SearchEnemy(); }
   }
 
}
