using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : Enemy, IDamagele
{
    public void GeiHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }
}
