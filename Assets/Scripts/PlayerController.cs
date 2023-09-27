using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rbplayer;
    public bool isGrounded;
    public Transform groundCheckCollider;
    const float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rbplayer.velocity = new Vector2(-5, rbplayer.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rbplayer.velocity = new Vector2(5, rbplayer.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        GrounCheck();
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
            rbplayer.velocity = new Vector2(0f, 8f);
        }
    }
}
