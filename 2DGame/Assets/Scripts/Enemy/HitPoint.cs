using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    int dir;
    public bool boomAvilable;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > collision.transform.position.x)
        {
            dir = -1;
        }
        else
            dir = 1;

        if (collision.CompareTag("Player") )
        {
            collision.GetComponent<IDamagele>().GeiHit(1);
            Debug.Log("gg");
            if (boomAvilable)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * 5, ForceMode2D.Impulse);
            }
            
        }
        if (collision.CompareTag("Bomb")&& boomAvilable)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * 10, ForceMode2D.Impulse);
        }
    }
  

}
