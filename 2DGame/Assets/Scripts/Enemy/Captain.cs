using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Enemy, IDamagele
{

    SpriteRenderer sprite; 
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

    public override void Init()
    {
        base.Init();
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();
        if (animState==0)
        {
            sprite.flipX = false;
        }
    }
    public override void SkillAction()
    {       
        base.SkillAction();
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Captain_skill"))
        {
            sprite.flipX = true;
            if (transform.position.x>targetPoint.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position,transform.position+Vector3.right,speed*1.5f*Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, speed * 1.5f * Time.deltaTime);
            }
        }
        else
        {
            sprite.flipX = false;
        }

    }

}
