using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {
    public float walkSpeed;
    public float jumpImpulse;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private Transform target;
    public Transform attackPoint;
    public LayerMask whatToAttack;
    public int damage;

    private Rigidbody2D body;


    private Vector2 movement;
    private float horInput;
    private bool IamIntheGround;
    private bool facingRight;

    private Animator anim;
    private float time;
    private float timeToAttack;


    // Use this for initialization
    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();
        this.movement = new Vector2();
        this.IamIntheGround = false;

        this.anim = this.GetComponent<Animator>();
        this.facingRight = true;

        this.time = 0;
        this.timeToAttack = 1;

        GameObject tmp = GameObject.FindGameObjectWithTag("Player"); 
        if (tmp != null)
        {
            this.target = tmp.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (this.target)
        {
            if (this.transform.position.x < this.target.position.x)
            {
                this.horInput = 1;
            }
            else if (this.transform.position.x > this.target.position.x)
            {
                this.horInput = -1;
            }
        }
        else
        {
            this.horInput = 0;

        }



        if (horInput != 0)
        {
            if ((this.horInput < 0) && (this.facingRight))
            {
                this.Flip();
                this.facingRight = false;

            }
            else if ((this.horInput > 0) && (!this.facingRight))
            {
                this.Flip();
                this.facingRight = true;
            }

            this.anim.SetFloat("HorSpeed", Mathf.Abs(this.horInput));
            this.anim.SetFloat("VertSpeed", Mathf.Abs(this.body.velocity.y));



            if (Physics2D.OverlapCircle(this.groundCheckPoint.position, 0.1f, this.whatIsGround))
            {
                this.IamIntheGround = true;
            }
            else
            {
                this.IamIntheGround = false;
            }

        }

    }

    void FixedUpdate()
    {
        this.movement = this.body.velocity;

        this.time += Time.deltaTime;
        if (this.time > this.timeToAttack)
        {
            this.time = 0;
            this.Detect();
        }


        this.movement.x = horInput * walkSpeed;


        if (this.IamIntheGround)
        {
            if (this.movement.y < -9)
            {
                this.movement.y = -9;
            }
        }
        this.body.velocity = this.movement;
    }

    void Flip()
    {
        //  Vector3 scale = this.transform.localScale;
        // scale.x = scale.x*(-1);
        // this.transform.localScale=scale;
        this.transform.Rotate(Vector3.up, 180);
    }

    

    public void OnGetKill()
    {
        GameMaster.current.AddPuntuation(100);

    }

    void Detect()
    {
        Collider2D tmp = Physics2D.OverlapCircle(this.attackPoint.position, 1f, this.whatToAttack);
        if (tmp)
        {
            if (tmp.gameObject.CompareTag("Player"))
            {
                tmp.gameObject.SendMessage("Hurt", damage);
            }
            else {
                if (this.IamIntheGround)
                {
                    Debug.Log("intento saltar");
                    this.movement.y = jumpImpulse;
                }
            }
        }
    }



}
