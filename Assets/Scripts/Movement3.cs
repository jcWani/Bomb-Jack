using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3 : MonoBehaviour
{
    [SerializeField] Rigidbody2D RBcharacter;
    [SerializeField] Collider2D StandingCollider;
    [SerializeField] float speed;
    bool facingRight = true;
    [SerializeField] bool isGrounded = false;
    [SerializeField] bool crouchPressed;
    float moveX;
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheckCollider;
    const float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    const float crouchSpeed = 0.2f;
    [SerializeField] Transform OHCheckCollider;
    const float OHgroundCheckRadius = 0.2f;


    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            crouchPressed = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouchPressed = false;
        }
    }
    private void FixedUpdate()
    {
        GrounCheck();
        Move(moveX, crouchPressed);
    }
    private void Move(float direction, bool crouchFlag)
    {
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(OHCheckCollider.position, OHgroundCheckRadius, groundLayer))
            {
                crouchFlag = true;
            }
        }

        StandingCollider.enabled = !crouchFlag;
        float xVelocity = direction * speed * 100 * Time.fixedDeltaTime;
        if (crouchFlag)
        {
            xVelocity *= crouchSpeed;
        }

        Vector2 targetVel = new Vector2(xVelocity, RBcharacter.velocity.y);
        RBcharacter.velocity = targetVel;
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
    }
    private void GrounCheck()
    {
        //Generate circle
        //Check if circle collide with ground
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            RBcharacter.velocity = new Vector2(0f, 1) * jumpForce;
        }
    }


}
