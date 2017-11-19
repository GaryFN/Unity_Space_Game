using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager current;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject JumpButton;
    public GameObject fireButton;
    public Camera touchPadCamera;
    private float radious;
    public LayerMask buttonsLayer;
    private int leftBtnId;
    private int rightBtnId;
    private int jumpBtnId;
    private int fireBtnId;

    private float horInput;
    private bool jumpInput;
    private bool shootInput;
    void Awake()
    {
        current = this;
    }
    // Use this for initialization
    void Start() {
        this.radious = 1f;
        this.leftBtnId = -1;
        this.rightBtnId = -1;
        this.jumpBtnId = -1;
        this.fireBtnId = -1;
        this.jumpInput = false;
        this.shootInput = false;
    }

    // Update is called once per frame
    void Update() {
        //pc
#if UNITY_STANDALONE_WIN
        this.horInput = Input.GetAxis("Horizontal");
        this.jumpInput = Input.GetKey(KeyCode.Space);
#endif
#if UNITY_ANDROID
        
        //android and ios

        if (Input.touchCount > 0)
        {
            Touch actual;
            for (int i = 0; i < Input.touchCount; i++)
            {
                actual = Input.GetTouch(i);
                if (actual.phase != TouchPhase.Stationary)
                {
                    this.TouchProcess(actual);
                }
            }
        }
#endif

    }
    public bool GetJump() {
        return this.jumpInput;
    }
    public float GetHorizontal() {
        return this.horInput;
    }
    public bool GetShooting() {
        return this.shootInput;
    }

    void TouchProcess(Touch touch) {

        Vector3 position = this.touchPadCamera.ScreenToWorldPoint(touch.position);
        Collider2D collider = Physics2D.OverlapCircle(position, radious,buttonsLayer);

        if (this.leftBtnId==touch.fingerId)
        {
            if ((collider==null) || (collider.gameObject!=leftButton)
                || (touch.phase== TouchPhase.Ended))
            {
                this.leftBtnId = -1;
                this.horInput = 0;

            }
        }

        if (this.rightBtnId == touch.fingerId)
        {
            if ((collider == null) || (collider.gameObject != rightButton)
                || (touch.phase == TouchPhase.Ended))
            {
                this.rightBtnId = -1;
                this.horInput = 0;
            }
        }


        if (this.jumpBtnId == touch.fingerId)
        {
            if ((collider == null) || (collider.gameObject != JumpButton)
                || (touch.phase == TouchPhase.Ended))
            {
                this.jumpBtnId = -1;
                this.jumpInput = false;
            }
        }


        if (this.fireBtnId == touch.fingerId)
        {
            if ((collider == null) || (collider.gameObject != fireButton)
                || (touch.phase == TouchPhase.Ended))
            {
                this.fireBtnId = -1;
                this.shootInput = false;
            }
        }

        // nuevo dedo!!!!!!!!!!!!!!!
        if (touch.phase!= TouchPhase.Ended)
        {
            if (collider !=null)
            {
                if (collider.gameObject==this.leftButton)
                {
                    this.leftBtnId = touch.fingerId;
                    this.horInput = -1;
                }
                else if (collider.gameObject==this.rightButton)
                {
                    this.rightBtnId = touch.fingerId;
                    this.horInput = 1;
                }
                else if (collider.gameObject==this.JumpButton)
                {
                    this.jumpBtnId = touch.fingerId;
                    this.jumpInput = true;
                }
                else if (collider.gameObject== this.fireButton)
                {
                    this.fireBtnId = touch.fingerId;
                    this.shootInput = true;
                }
            }
        }
    }
}
