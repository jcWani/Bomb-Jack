using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUpdated : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RBplayer;
    [SerializeField] private Animator anim;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float FallSpeed;
    [SerializeField] float DiveSpeed;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float hurtForce = 10f;

    public int bombcollectible;
    private Collider2D coll;

    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;
    
    float moveX;

    // Start is called before the first frame update
    void Start()
    {
        RBplayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state != State.hurt)
        {
            moveX = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Glide();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Dive();
            }
        }

        WhatVelState();
        anim.SetInteger("State", (int)state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BombCollectible")
        {
            Destroy(collision.gameObject);
            bombcollectible += 1;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D enemyCollision)
    {
        if(enemyCollision.gameObject.tag == "Enemy")
        {
            if(state == State.falling)
            {
                Destroy(enemyCollision.gameObject);
            }
            else
            {
                state = State.hurt;
                if (enemyCollision.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right therefore should be damaged and move left
                    RBplayer.velocity = new Vector2(-hurtForce, RBplayer.velocity.y);
                }
                else
                {
                    //Enemy is to my left therefore i Should be damaged and move right
                    RBplayer.velocity = new Vector2(hurtForce, RBplayer.velocity.y);
                }
            }
            
        }
    }



    private void FixedUpdate()
    {
        GroundCheck();
        Move(moveX);
    }

    private void Move(float direction)
    {
        float xVelocity = direction * speed * 100 * Time.fixedDeltaTime;
        Vector2 targetVel = new Vector2(xVelocity, RBplayer.velocity.y);
        RBplayer.velocity = targetVel;
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] collider = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.5f, groundLayers);
        if (collider.Length > 0)
        {
            isGrounded = true;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            RBplayer.velocity = new Vector2(0f, 1) * jumpForce;
            state = State.jumping;
        }
    }

    private void Glide()
    {
        if (!isGrounded)
        {
            RBplayer.velocity = new Vector2(0f, -1) * FallSpeed;
        }
    }

    private void Dive()
    {
        if (!isGrounded)
        {
            RBplayer.velocity = new Vector2(0f, -1) * DiveSpeed;
        }
    }

    private void WhatVelState()
    {

        if(state == State.jumping)
        {
            if(RBplayer.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (isGrounded)
            {
                state = State.idle;
            }
        }
        else if(state == State.hurt)
        {
            if (Mathf.Abs(RBplayer.velocity.x)  < .1f)
            {
                state = State.idle;
            }
        }
        else if(Mathf.Abs(RBplayer.velocity.x) > Mathf.Epsilon)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }

    }
}


