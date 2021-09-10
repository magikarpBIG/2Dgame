using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamagele
{
    private Rigidbody2D rb;
    private Animator anim;
    private FixedJoystick joystick;

    public float speed;
    public float jumpForce;

    [Header("Player State")]
    public float health;
    public float damage;
    public bool isDead;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("States Check")]
    public bool isGround;
    public bool canJump;
    public bool isJump;

    [Header("Jump Fx")]
    public GameObject jumpFx;
    public GameObject landFx;

    [Header("Attack Settings")]
    public GameObject bombPrefb;
    public float nextAttack = 0;
    public float attackRate;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();

        GameManager.instance.IsPlayer(this);
        health = GameManager.instance.LoadHealth();
        UIManager.instance.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    { 
        anim.SetBool("dead", isDead);
        if (isDead)
        {
            return;
        }
        CheckInput();
       
    }
    public void FixedUpdate()
    {
        if (isDead )
        {
            rb.velocity = Vector2.zero;
            return;
        }
        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Jump")&&isGround)
        {
            canJump = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    void Movement()
    {
        //键盘操作
        // float horizontalInput = Input.GetAxis("Horizontal");
       //float horizontalInput = Input.GetAxisRaw("Horizontal");
        //操作杆
        float horizontalInput = joystick.Horizontal;
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        //if (horizontalInput!=0)
        //{
        //    transform.localScale = new Vector3(horizontalInput,1,1);
        //}

        if (horizontalInput>0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }
     void Jump()
    {
          if (canJump)
        {
            isJump = true;
            jumpFx.SetActive(true);
            jumpFx.transform.position = transform.position + new Vector3(0, -0.45f, 0);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 4;

            canJump = false;
        }
    }

    public void ButtonJump()
    {
        canJump = true;
        if (!isGround)
        {
            canJump = false;
        }
    }

    public void Attack() {
        if (Time.time >nextAttack)
        {
            Instantiate(bombPrefb, transform.position, bombPrefb.transform.rotation);

            nextAttack = Time.time + attackRate;
        }
    }

    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position,checkRadius,groundLayer);
        if (isGround)
        {   
            rb.gravityScale = 1;
            isJump = false;
        }
    }

    public void LandFx()
    {
        landFx.SetActive(true);
        landFx.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position,checkRadius);
    }

    public void GeiHit(float damage)
    {
        if (!anim.GetCurrentAnimatorStateInfo(1).IsName("player_hit"))
        {

         health -= damage;
            if (health <1)
            {
                health = 0;
                isDead = true;
            }
        anim.SetTrigger("hit");
            UIManager.instance.UpdateHealth(health);       
        }
    }
}
