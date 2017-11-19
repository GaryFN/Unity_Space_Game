using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float walkSpeed;
    public float jumpImpulse;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private Weapon UpdateWeapon;
    public Transform Recojerobjeto;
    public LayerMask whatObjectis;



    private Rigidbody2D body;
    private Vector2 movement;
    private float horInput;
    private bool IamIntheGround;
    private bool facingRight;

    private Animator anim;


    private bool jumpInput;
	// Use this for initialization
	void Start () {
        this.body = this.GetComponent<Rigidbody2D>();
        this.movement = new Vector2();
        this.IamIntheGround = false;

        this.anim = this.GetComponent<Animator>();
        this.facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        this.horInput = InputManager.current.GetHorizontal();
        this.jumpInput =InputManager.current.GetJump() ;

        if ((this.horInput < 0) && (this.facingRight))
        {
            this.Flip();
            this.facingRight = false;

        } else if ((this.horInput>0) && (!this.facingRight))
        {
            this.Flip();
            this.facingRight = true;
        }

        this.anim.SetFloat("HorSpeed",Mathf.Abs(this.horInput));
        this.anim.SetFloat("VertSpeed", Mathf.Abs(this.body.velocity.y));



        if (Physics2D.OverlapCircle(this.groundCheckPoint.position, 0.2f, this.whatIsGround))
        {
            this.IamIntheGround = true;
        }
        else
        {
            this.IamIntheGround = false;
        }
	}

    void FixedUpdate()
    {
        this.movement = this.body.velocity;

        this.movement.x = horInput * walkSpeed;

        Detect();
        if (this.jumpInput && this.IamIntheGround) {
            this.movement.y = jumpImpulse;
        }

        if (this.IamIntheGround)
        {
            if (this.movement.y < -9)    
        {
                this.movement.y = -9;
            }
        }
        this.body.velocity = this.movement;
    }

    void Flip() {
        //  Vector3 scale = this.transform.localScale;
        // scale.x = scale.x*(-1);
        // this.transform.localScale=scale;
        this.transform.Rotate(Vector3.up,180);
    }

    public void OnGetKill() {
        GameMaster.current.GameOver();
    }

    void Detect()
    {
        Collider2D tmp = Physics2D.OverlapCircle(this.Recojerobjeto.position, 0.4f, this.whatObjectis);
        if (tmp)
        {
            if (tmp.gameObject.CompareTag("nextLevel"))
            {
               

                Debug.Log("siguiente nivel");
                System.Threading.Thread.Sleep(1000);
                Application.LoadLevel(3);
            }
            if (tmp.gameObject.CompareTag("nextLevelFinal"))
            {
                Debug.Log("siguiente nivel");
                System.Threading.Thread.Sleep(1000);
                Application.LoadLevel(1);
            }
            if (tmp.gameObject.CompareTag("Food"))
            {
                
                this.gameObject.SendMessage("FoodAmmo");
                Destroy(GameObject.FindGameObjectWithTag("Food"));
            }
            if (tmp.gameObject.CompareTag("Food1"))
            {

                this.gameObject.SendMessage("FoodAmmo");
                Destroy(GameObject.FindGameObjectWithTag("Food1"));
            }
            if (tmp.gameObject.CompareTag("Food2"))
            {

                this.gameObject.SendMessage("FoodAmmo");
                Destroy(GameObject.FindGameObjectWithTag("Food2"));
            }
        }
    }

}
