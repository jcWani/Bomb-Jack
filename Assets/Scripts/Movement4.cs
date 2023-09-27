using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement4 : MonoBehaviour
{ 

    [SerializeField] private Rigidbody2D RBplayer;
    [SerializeField] private Animator anim;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float FallSpeed;
    [SerializeField] float DiveSpeed;
    [SerializeField] float hurtForce;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayers;
    bool facingRight = true;

    [SerializeField] TextMeshProUGUI scoreManager;
    [SerializeField] int bombcollectible = 0;
    GameObject[] bomb;
    [SerializeField] GameObject Explosion;
    [SerializeField] float spawnBombAnim = 5f;
    float nextSpawn = 0.0f;

    [SerializeField] Image[] lives;
    [SerializeField] int livesRemaining;
    GameObject[] Enemies;
    [SerializeField] GameObject[] EnemiesAfter;
    [SerializeField] GameObject[] Upss;

    float moveX;

// Start is called before the first frame update
    void Start()
    {
        RBplayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        moveX = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveX));

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        else if (Input.GetButton("Gliding"))
        {
            Glide();
        }
        else if (Input.GetButton("Dive"))
        {
            Dive();
        }


        
        anim.SetFloat("yVelocity", RBplayer.velocity.y);
        BombSpawn();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BombCollectible")
        {
            Destroy(collision.gameObject);
            bombcollectible += 1;
            scoreManager.text = bombcollectible.ToString();
            if ( bombcollectible == 10)
            {
                StartCoroutine(SpawnUpss());
            }
        }
        else if (collision.tag == "BombExplode")
        {
           Destroy(collision.gameObject);
           bombcollectible += 2;
           scoreManager.text = bombcollectible.ToString();
            if (bombcollectible == 10)
            {
                StartCoroutine(SpawnUpss());
            }
        }
        else if (collision.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject a in Enemies)
            {
                a.SetActive(false);
            }
            StartCoroutine(ResetPower());
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            LoseLife();
            if (col.gameObject.transform.position.x > transform.position.x)
            {
                RBplayer.velocity = new Vector2(-1, RBplayer.velocity.y) * hurtForce;
            }
            else
            {
                RBplayer.velocity = new Vector2(1, RBplayer.velocity.y) * hurtForce;
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
            facingRight = false;
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
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

        anim.SetBool("isJumping", !isGrounded); 
    }

    private void Jump()
    {
        if (isGrounded)
        {
            RBplayer.velocity = new Vector2(0f, 1) * jumpForce;
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

    private void BombSpawn()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnBombAnim;
            Vector2 bombSpawn = new Vector2(Random.Range(-8.4f, 8.4f), transform.position.y);
            GameObject theExplosion = Instantiate(Explosion, bombSpawn, Quaternion.identity);
            Destroy(theExplosion, 5f);
        }

    }

    public void LoseLife()
    {
        livesRemaining--;
        lives[livesRemaining].enabled = false;

        if (livesRemaining == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5f);
        foreach (GameObject b in EnemiesAfter)
        {
            b.SetActive(true);
        }
    }
    private IEnumerator SpawnUpss()
    {
        yield return new WaitForSeconds(0f);
        foreach (GameObject b in Upss)
        {
            b.SetActive(true);
        }
    }

}

