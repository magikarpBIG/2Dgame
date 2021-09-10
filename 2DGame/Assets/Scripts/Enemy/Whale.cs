using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : Enemy, IDamagele
{

    public float scale;
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

    public void Swalow()
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
        targetPoint.gameObject.SetActive(false);
        transform.localScale *= scale;
    }
}
