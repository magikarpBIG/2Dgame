using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool ending=false;
    Animator anim;
    BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        GameManager.instance.IsExitDoor(this);
        coll.enabled = false;
    }

    public void OpenDoor()
    {
        anim.Play("opening");
        coll.enabled = true;
    }
   public  void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ending)
            {
                GameManager.instance.GoToMainMenu();
            }
            else
                GameManager.instance.NextLevel();
        }
    }
}
