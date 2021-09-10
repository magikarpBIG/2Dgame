using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;

    public float startTime;
    public float waitTime;
    public float bombForce;
    private Collider2D coll;
    private Rigidbody2D rb;

    [Header("Check")]
    public float radius;
    public LayerMask targetLayer;
    void Start()
    {
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Bomb_off"))
        {
            if (Time.time>startTime+waitTime)
            {
                anim.Play("Bomb_explotion");
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
    public void Explotion()
    {
        coll.enabled = false;
        Collider2D[]aroundObjects=Physics2D.OverlapCircleAll(transform.position,radius,targetLayer);

        rb.gravityScale = 0;
        foreach (var item in aroundObjects)
        {
            Vector3 pos = transform.position - item.transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((-pos+Vector3.up) * bombForce, ForceMode2D.Impulse);
            if (item.CompareTag("Bomb")&&item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bomb_off"))
            {
                item.GetComponent<Bomb>().TurnOn();
            }
            if (item.CompareTag("Player"))
            {
                item.GetComponent<IDamagele>().GeiHit(2);
            }

        }
    
    }
    public void FinishA()
    {
        Destroy(gameObject);
        
    }
    public void TurnOff()
    {
        anim.Play("Bomb_off");
        gameObject.layer = LayerMask.NameToLayer("Npc");
    }
    public void TurnOn()
    {
        startTime = Time.time;
        anim.Play("Bomb On");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }
}
