using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy,IDamagele
{
   

    public void GeiHit(float damage)
    {
        health -= damage;
        if (health<1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }

    public override void Init()
    {
        base.Init();
       
    }

    public void SetOff()
    {
        targetPoint.GetComponent<Bomb>()?.TurnOff();
    }

   
}
